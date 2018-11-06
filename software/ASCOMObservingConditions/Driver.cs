//tabs=4
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;

using ASCOM;
using ASCOM.Astrometry;
using ASCOM.Astrometry.AstroUtils;
using ASCOM.Utilities;
using ASCOM.DeviceInterface;
using System.Globalization;
using System.Collections;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ASCOM.KomaHub
{
    //
    // Your driver's DeviceID is ASCOM.KomaHub.ObservingConditions
    //
    // The Guid attribute sets the CLSID for ASCOM.KomaHub.ObservingConditions
    // The ClassInterface/None addribute prevents an empty interface called
    // _KomaHub from being created and used as the [default] interface
    //
    // TODO Replace the not implemented exceptions with code to implement the function or
    // throw the appropriate ASCOM exception.
    //

    /// <summary>
    /// ASCOM ObservingConditions Driver for KomaHub.
    /// </summary>
    [Guid("baaa0aa8-a801-4f0e-a46e-3504fb7e446c")]
    [ClassInterface(ClassInterfaceType.None)]
    public class ObservingConditions : IObservingConditions
    {
        /// <summary>
        /// ASCOM DeviceID (COM ProgID) for this driver.
        /// The DeviceID is used by ASCOM applications to load the driver at runtime.
        /// </summary>
        internal static string driverID = "ASCOM.KomaHub.ObservingConditions";
        /// <summary>
        /// Driver description that displays in the ASCOM Chooser.
        /// </summary>
        private static string driverDescription = "KomaHub ObservingConditions Driver";

        internal static string serverAddress = "http://localhost:6563/sensors";

        internal enum AmbientTemperatureSource
        {
            PTH = 0,
            TEMP1 = 1,
            TEMP2 = 2,
            TEMP3 = 3,
            TEMP4 = 4,
            CLOUDSENSOR = 5
        };

        internal static AmbientTemperatureSource ambientTemperatureSource = AmbientTemperatureSource.PTH;

        // Weather data
        private Double temperature = Double.NaN;
        private Double humidity = Double.NaN;
        private Double pressure = Double.NaN;
        private Double dewpoint = Double.NaN;
        private Double cloudCover = Double.NaN;
        private Double windspeed = Double.NaN;
        private Double winddir = Double.NaN;
        private Double windgust = Double.NaN;
        private Double starfwhm = Double.NaN;
        private Double sqm = Double.NaN;
        private Double skytemp = Double.NaN;
        private Double skybrightness = Double.NaN;
        private Double rain = Double.NaN;

        private DateTime lastUpdate;

        /// <summary>
        /// Private variable to hold the connected state
        /// </summary>
        private bool connectedState;

        private TimerCallback updateTimerDelegate;
        private System.Threading.Timer updateTimer;

        /// <summary>
        /// Private variable to hold an ASCOM Utilities object
        /// </summary>
        private Util utilities;

        /// <summary>
        /// Private variable to hold an ASCOM AstroUtilities object to provide the Range method
        /// </summary>
        private AstroUtils astroUtilities;

        /// <summary>
        /// Variable to hold the trace logger object (creates a diagnostic log file with information that you specify)
        /// </summary>
        internal static TraceLogger tl;

        /// <summary>
        /// Initializes a new instance of the <see cref="KomaHub"/> class.
        /// Must be public for COM registration.
        /// </summary>
        public ObservingConditions()
        {
            tl = new TraceLogger("", "KomaHub");
#if DEBUG
            // Enable logging in debug mode
            tl.Enabled = true;
#endif
            ReadProfile(); // Read device configuration from the ASCOM Profile store

            tl.LogMessage("ObservingConditions", "Starting initialisation");

            connectedState = false; // Initialise connected to false
            utilities = new Util(); //Initialise util object
            astroUtilities = new AstroUtils(); // Initialise astro utilities object

            updateTimerDelegate = new TimerCallback(UpdateObservingConditionsData);

            tl.LogMessage("ObservingConditions", "Completed initialisation");
        }


        //
        // PUBLIC COM INTERFACE IObservingConditions IMPLEMENTATION
        //

#region Common properties and methods.

        /// <summary>
        /// Displays the Setup Dialog form.
        /// If the user clicks the OK button to dismiss the form, then
        /// the new settings are saved, otherwise the old values are reloaded.
        /// THIS IS THE ONLY PLACE WHERE SHOWING USER INTERFACE IS ALLOWED!
        /// </summary>
        public void SetupDialog()
        {
            // consider only showing the setup dialog if not connected
            // or call a different dialog if connected
            if (IsConnected)
                System.Windows.Forms.MessageBox.Show("Already connected, just press OK");

            using (SetupDialogForm F = new SetupDialogForm())
            {
                var result = F.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    WriteProfile(); // Persist device configuration values to the ASCOM Profile store
                }
            }
        }

        public ArrayList SupportedActions
        {
            get
            {
                tl.LogMessage("SupportedActions Get", "Returning empty arraylist");
                return new ArrayList();
            }
        }

        public string Action(string actionName, string actionParameters)
        {
            LogMessage("", "Action {0}, parameters {1} not implemented", actionName, actionParameters);
            throw new ASCOM.ActionNotImplementedException("Action " + actionName + " is not implemented by this driver");
        }

        public void CommandBlind(string command, bool raw)
        {
            CheckConnected("CommandBlind");
            throw new ASCOM.MethodNotImplementedException("CommandBlind");
        }

        public bool CommandBool(string command, bool raw)
        {
            CheckConnected("CommandBool");
            throw new ASCOM.MethodNotImplementedException("CommandBool");
        }

        public string CommandString(string command, bool raw)
        {
            CheckConnected("CommandString");
            throw new ASCOM.MethodNotImplementedException("CommandString");
        }

        public void Dispose()
        {
            // Clean up the tracelogger and util objects
            tl.Enabled = false;
            tl.Dispose();
            tl = null;
            utilities.Dispose();
            utilities = null;
            astroUtilities.Dispose();
            astroUtilities = null;
        }

        public bool Connected
        {
            get
            {
                LogMessage("Connected", "Get {0}", IsConnected);
                return IsConnected;
            }
            set
            {
                tl.LogMessage("Connected", "Set {0}", value);
                if (value == IsConnected)
                    return;

                if (value)
                {
                    connectedState = true;
                    LogMessage("Connected Set", "Connecting to server {0}", serverAddress);
                    UpdateObservingConditionsData(null);

                    if (updateTimer == null)
                    {
                        updateTimer = new System.Threading.Timer(updateTimerDelegate, null, 0, (10 * 1000));
                    }
                }
                else
                {
                    connectedState = false;
                    LogMessage("Connected Set", "Disconnecting from server {0}", serverAddress);

                    if (updateTimer != null)
                    {
                        updateTimer.Dispose();
                        updateTimer = null;
                    }
                }
            }
        }

        public string Description
        {
            get
            {
                tl.LogMessage("Description Get", driverDescription);
                return driverDescription;
            }
        }

        public string DriverInfo
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                string driverInfo = "Information about the driver itself. Version: " + String.Format(CultureInfo.InvariantCulture, "{0}.{1}", version.Major, version.Minor);
                tl.LogMessage("DriverInfo Get", driverInfo);
                return driverInfo;
            }
        }

        public string DriverVersion
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                string driverVersion = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", version.Major, version.Minor);
                tl.LogMessage("DriverVersion Get", driverVersion);
                return driverVersion;
            }
        }

        public short InterfaceVersion
        {
            // set by the driver wizard
            get
            {
                LogMessage("InterfaceVersion Get", "1");
                return Convert.ToInt16("1");
            }
        }

        public string Name
        {
            get
            {
                string name = "KomaHub ObservingConditions";
                tl.LogMessage("Name Get", name);
                return name;
            }
        }

#endregion

#region IObservingConditions Implementation

        /// <summary>
        /// Gets and sets the time period over which observations wil be averaged
        /// </summary>
        /// <remarks>
        /// Get must be implemented, if it can't be changed it must return 0
        /// Time period (hours) over which the property values will be averaged 0.0 =
        /// current value, 0.5= average for the last 30 minutes, 1.0 = average for the
        /// last hour
        /// </remarks>
        public double AveragePeriod
        {
            get
            {
                LogMessage("AveragePeriod", "get - 0");
                return 0;
            }
            set
            {
                LogMessage("AveragePeriod", "set - {0}", value);
                if (value != 0)
                    throw new InvalidValueException("AveragePeriod", value.ToString(), "0 only");
            }
        }

        /// <summary>
        /// Amount of sky obscured by cloud
        /// </summary>
        /// <remarks>0%= clear sky, 100% = 100% cloud coverage</remarks>
        public double CloudCover
        {
            get
            {
                if (Double.IsNaN(cloudCover))
                    throw new PropertyNotImplementedException("CloudCover");

                return cloudCover;
            }
        }

        /// <summary>
        /// Atmospheric dew point at the observatory in deg C
        /// </summary>
        /// <remarks>
        /// Normally optional but mandatory if <see cref=" ASCOM.DeviceInterface.IObservingConditions.Humidity"/>
        /// Is provided
        /// </remarks>
        public double DewPoint
        {
            get
            {
                if (Double.IsNaN(dewpoint))
                    throw new PropertyNotImplementedException("DewPoint");
                return dewpoint;
            }
        }

        /// <summary>
        /// Atmospheric relative humidity at the observatory in percent
        /// </summary>
        /// <remarks>
        /// Normally optional but mandatory if <see cref="ASCOM.DeviceInterface.IObservingConditions.DewPoint"/> 
        /// Is provided
        /// </remarks>
        public double Humidity
        {
            get
            {
                if (Double.IsNaN(humidity))
                    throw new PropertyNotImplementedException("Humidity");
                return humidity;
            }
        }

        /// <summary>
        /// Atmospheric pressure at the observatory in hectoPascals (mB)
        /// </summary>
        /// <remarks>
        /// This must be the pressure at the observatory and not the "reduced" pressure
        /// at sea level. Please check whether your pressure sensor delivers local pressure
        /// or sea level pressure and adjust if required to observatory pressure.
        /// </remarks>
        public double Pressure
        {
            get
            {
                if (Double.IsNaN(pressure))
                    throw new PropertyNotImplementedException("Pressure");
                return pressure;
            }
        }

        /// <summary>
        /// Rain rate at the observatory
        /// </summary>
        /// <remarks>
        /// This property can be interpreted as 0.0 = Dry any positive nonzero value
        /// = wet.
        /// </remarks>
        public double RainRate
        {
            get
            {
                if (Double.IsNaN(rain))
                    throw new PropertyNotImplementedException("RainRate");
                return rain;
            }
        }

        /// <summary>
        /// Forces the driver to immediatley query its attached hardware to refresh sensor
        /// values
        /// </summary>
        public void Refresh()
        {
            UpdateObservingConditionsData(null);
        }

        /// <summary>
        /// Provides a description of the sensor providing the requested property
        /// </summary>
        /// <param name="PropertyName">Name of the property whose sensor description is required</param>
        /// <returns>The sensor description string</returns>
        /// <remarks>
        /// PropertyName must be one of the sensor properties, 
        /// properties that are not implemented must throw the MethodNotImplementedException
        /// </remarks>
        public string SensorDescription(string PropertyName)
        {
            switch (PropertyName.Trim().ToLowerInvariant())
            {
                case "averageperiod":
                    return "Average period in hours, immediate values are only available";
                case "dewpoint":
                    return "Humidity";
                case "pressure":
                    return "Pressure";
                case "temperature":
                    return "Temperature";
                case "winddirection":
                    return "Wind direction";
                case "windspeed":
                    return "Wind speed";
                case "humidity":
                    return "Humidity";
                case "cloudcover":
                    return "Cloud cover";
                case "rainrate":
                    return "Rain rate";
                case "skybrightness":
                    return "Sky brightness (lux)";
                case "skyquality":
                    return "Sky quality";
                case "starfwhm":
                    return "Star FWHM";
                case "skytemperature":
                    return "Sky temperature";
                case "windgust":
                    return "Wind gust";
                default:
                    LogMessage("SensorDescription", "{0} - unrecognised", PropertyName);
                    throw new ASCOM.InvalidValueException("SensorDescription(" + PropertyName + ")");
            }
        }

        /// <summary>
        /// Sky brightness at the observatory
        /// </summary>
        public double SkyBrightness
        {
            get
            {
                if (Double.IsNaN(skybrightness))
                    throw new PropertyNotImplementedException("SkyBrightness");
                return skybrightness;
            }
        }

        /// <summary>
        /// Sky quality at the observatory
        /// </summary>
        public double SkyQuality
        {
            get
            {
                if (Double.IsNaN(sqm))
                    throw new PropertyNotImplementedException("SkyQuality");
                return sqm;
            }
        }

        /// <summary>
        /// Seeing at the observatory
        /// </summary>
        public double StarFWHM
        {
            get
            {
                if (Double.IsNaN(starfwhm))
                    throw new PropertyNotImplementedException("StarFWHM");
                return starfwhm;
            }
        }

        /// <summary>
        /// Sky temperature at the observatory in deg C
        /// </summary>
        public double SkyTemperature
        {
            get
            {
                if (Double.IsNaN(skytemp))
                    throw new PropertyNotImplementedException("SkyTemperature");
                return skytemp;
            }
        }

        /// <summary>
        /// Temperature at the observatory in deg C
        /// </summary>
        public double Temperature
        {
            get
            {
                if (Double.IsNaN(temperature))
                    throw new PropertyNotImplementedException("Temperature");
                return temperature;
            }
        }

        /// <summary>
        /// Provides the time since the sensor value was last updated
        /// </summary>
        /// <param name="PropertyName">Name of the property whose time since last update Is required</param>
        /// <returns>Time in seconds since the last sensor update for this property</returns>
        /// <remarks>
        /// PropertyName should be one of the sensor properties Or empty string to get
        /// the last update of any parameter. A negative value indicates no valid value
        /// ever received.
        /// </remarks>
        public double TimeSinceLastUpdate(string PropertyName)
        {
            // the checks can be removed if all properties have the same time.
            if (!string.IsNullOrEmpty(PropertyName))
            {
                switch (PropertyName.Trim().ToLowerInvariant())
                {
                    // break or return the time on the properties that are implemented
                    case "averageperiod":
                    case "humidity":
                    case "pressure":
                    case "temperature":
                    case "winddirection":
                    case "windspeed":
                    case "cloudcover":
                    case "rainrate":
                    case "dewpoint":
                    case "skybrightness":
                    case "skyquality":
                    case "starfwhm":
                    case "skytemperature":
                    case "windgust":
                        return (DateTime.Now - lastUpdate).Seconds;
                    default:
                        LogMessage("TimeSinceLastUpdate", "{0} - unrecognised", PropertyName);
                        throw new ASCOM.InvalidValueException("SensorDescription(" + PropertyName + ")");
                }
            }

            return (DateTime.Now - lastUpdate).Seconds;
        }

        /// <summary>
        /// Wind direction at the observatory in degrees
        /// </summary>
        /// <remarks>
        /// 0..360.0, 360=N, 180=S, 90=E, 270=W. When there Is no wind the driver will
        /// return a value of 0 for wind direction
        /// </remarks>
        public double WindDirection
        {
            get
            {
                if (Double.IsNaN(winddir))
                    throw new PropertyNotImplementedException("WindDirection");
                return winddir;
            }
        }

        /// <summary>
        /// Peak 3 second wind gust at the observatory over the last 2 minutes in m/s
        /// </summary>
        public double WindGust
        {
            get
            {
                if (Double.IsNaN(windgust))
                    throw new PropertyNotImplementedException("WindGust");
                return windgust;
            }
        }

        /// <summary>
        /// Wind speed at the observatory in m/s
        /// </summary>
        public double WindSpeed
        {
            get
            {
                if (Double.IsNaN(windspeed))
                    throw new PropertyNotImplementedException("WindSpeed");
                return windspeed;
            }
        }

#endregion

#region private methods

        private void UpdateObservingConditionsData(object state)
        {
            var request = WebRequest.Create(serverAddress) as HttpWebRequest;
            try
            {
                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception(String.Format("Server error (HTTP {0}: {1}).",
                            response.StatusCode,
                            response.StatusDescription));
                    }

                    string json;
                    using (var responseStream = response.GetResponseStream())
                    {
                        using (var reader = new StreamReader(responseStream, System.Text.Encoding.UTF8))
                        {
                            json = reader.ReadToEnd();
                        }
                    }

                    Dictionary<string, object> values = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                    if (values.ContainsKey("pth"))
                    {
                        Dictionary<string, float> pthvalues = ((JObject)values["pth"]).ToObject<Dictionary<string, float>>(); ;
                        if (ambientTemperatureSource == AmbientTemperatureSource.PTH)
                            temperature = pthvalues["temperature"];
                        humidity = pthvalues["humidity"];
                        dewpoint = pthvalues["dewpoint"];
                        pressure = pthvalues["pressure"];
                    }
                    if (values.ContainsKey("skyquality"))
                    {
                        Dictionary<string, float> skyqualityvalues = ((JObject)values["skyquality"]).ToObject<Dictionary<string, float>>(); ;
                        sqm = (float)skyqualityvalues["magnitude"];
                    }
                    if (values.ContainsKey("skytemperature"))
                    {
                        Dictionary<string, float> skytemperaturevalues = ((JObject)values["skytemperature"]).ToObject<Dictionary<string, float>>(); ;
                        skytemp = skytemperaturevalues["sky"];
                        if (ambientTemperatureSource == AmbientTemperatureSource.CLOUDSENSOR)
                            temperature = skytemperaturevalues["ambient"];

                        cloudCover = 100 * (1.0 - (skytemperaturevalues["ambient"] - skytemperaturevalues["sky"] - 5) / 10);
                        cloudCover = Math.Max(Math.Min(100, cloudCover), 0);
                    }
                    if (values.ContainsKey("temperatures"))
                    {
                        List<float> temps = ((JObject)values["temperatures"]).ToObject<List<float>>();
                        if (ambientTemperatureSource == AmbientTemperatureSource.TEMP1 && temps.Count >= 1)
                            temperature = temps[0];
                        if (ambientTemperatureSource == AmbientTemperatureSource.TEMP2 && temps.Count >= 2)
                            temperature = temps[1];
                        if (ambientTemperatureSource == AmbientTemperatureSource.TEMP3 && temps.Count >= 3)
                            temperature = temps[2];
                        if (ambientTemperatureSource == AmbientTemperatureSource.TEMP4 && temps.Count >= 4)
                            temperature = temps[3];
                    }

                    lastUpdate = DateTime.Now;
                }
            } catch( Exception e)
            {
                LogMessage("Update", "Error: " + e.Message);
            }
        }

        private Double parseValue(Dictionary<string, object> values, string key)
        {
            if (!values.ContainsKey(key))
                return Double.NaN;
            else
                return Double.Parse(values[key].ToString(), CultureInfo.InvariantCulture);
        }

#endregion

#region Private properties and methods
        // here are some useful properties and methods that can be used as required
        // to help with driver development

#region ASCOM Registration

        // Register or unregister driver for ASCOM. This is harmless if already
        // registered or unregistered. 
        //
        /// <summary>
        /// Register or unregister the driver with the ASCOM Platform.
        /// This is harmless if the driver is already registered/unregistered.
        /// </summary>
        /// <param name="bRegister">If <c>true</c>, registers the driver, otherwise unregisters it.</param>
        private static void RegUnregASCOM(bool bRegister)
        {
            using (var P = new ASCOM.Utilities.Profile())
            {
                P.DeviceType = "ObservingConditions";
                if (bRegister)
                {
                    P.Register(driverID, driverDescription);
                }
                else
                {
                    P.Unregister(driverID);
                }
            }
        }

        /// <summary>
        /// This function registers the driver with the ASCOM Chooser and
        /// is called automatically whenever this class is registered for COM Interop.
        /// </summary>
        /// <param name="t">Type of the class being registered, not used.</param>
        /// <remarks>
        /// This method typically runs in two distinct situations:
        /// <list type="numbered">
        /// <item>
        /// In Visual Studio, when the project is successfully built.
        /// For this to work correctly, the option <c>Register for COM Interop</c>
        /// must be enabled in the project settings.
        /// </item>
        /// <item>During setup, when the installer registers the assembly for COM Interop.</item>
        /// </list>
        /// This technique should mean that it is never necessary to manually register a driver with ASCOM.
        /// </remarks>
        [ComRegisterFunction]
        public static void RegisterASCOM(Type t)
        {
            RegUnregASCOM(true);
        }

        /// <summary>
        /// This function unregisters the driver from the ASCOM Chooser and
        /// is called automatically whenever this class is unregistered from COM Interop.
        /// </summary>
        /// <param name="t">Type of the class being registered, not used.</param>
        /// <remarks>
        /// This method typically runs in two distinct situations:
        /// <list type="numbered">
        /// <item>
        /// In Visual Studio, when the project is cleaned or prior to rebuilding.
        /// For this to work correctly, the option <c>Register for COM Interop</c>
        /// must be enabled in the project settings.
        /// </item>
        /// <item>During uninstall, when the installer unregisters the assembly from COM Interop.</item>
        /// </list>
        /// This technique should mean that it is never necessary to manually unregister a driver from ASCOM.
        /// </remarks>
        [ComUnregisterFunction]
        public static void UnregisterASCOM(Type t)
        {
            RegUnregASCOM(false);
        }

#endregion

        /// <summary>
        /// Returns true if there is a valid connection to the driver hardware
        /// </summary>
        private bool IsConnected
        {
            get
            {
                // TODO check that the driver hardware connection exists and is connected to the hardware
                return connectedState;
            }
        }

        /// <summary>
        /// Use this function to throw an exception if we aren't connected to the hardware
        /// </summary>
        /// <param name="message"></param>
        private void CheckConnected(string message)
        {
            if (!IsConnected)
            {
                throw new ASCOM.NotConnectedException(message);
            }
        }

        /// <summary>
        /// Read the device configuration from the ASCOM Profile store
        /// </summary>
        internal void ReadProfile()
        {
            using (Profile driverProfile = new Profile())
            {
                driverProfile.DeviceType = "ObservingConditions";
                ambientTemperatureSource = (AmbientTemperatureSource)Convert.ToInt32(driverProfile.GetValue(driverID, "ambientTemperatureSource", string.Empty, "0"));
            }
        }

        /// <summary>
        /// Write the device configuration to the  ASCOM  Profile store
        /// </summary>
        internal void WriteProfile()
        {
            using (Profile driverProfile = new Profile())
            {
                driverProfile.DeviceType = "ObservingConditions";
                driverProfile.WriteValue(driverID, "ambientTemperatureSource", ""+(int)ambientTemperatureSource);
            }
        }

        /// <summary>
        /// Log helper function that takes formatted strings and arguments
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        internal static void LogMessage(string identifier, string message, params object[] args)
        {
            var msg = string.Format(message, args);
            tl.LogMessage(identifier, msg);
        }
#endregion
    }
}
