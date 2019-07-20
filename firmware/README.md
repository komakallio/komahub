# KomaHub firmware

## Compiling the firmware

In order for the firmware to use a custom USB Product ID, the usb_rawhid core must be manually modified as there
is no way to do this from Arduino/PlatformIO code.

`~/.platformio/packages/framework-arduinoteensy/cores/usb_rawhid/usb_private.h`:

```
#define VENDOR_ID               0x1209
#define PRODUCT_ID              0x4242
```

```
#ifndef STR_PRODUCT
#define STR_PRODUCT             L"KomaHub Remote Power Switch"
#endif
#ifndef STR_RAWHID
#define STR_RAWHID              L"KomaHub Remote Power Switch"
#endif
```

Also add the following definition to let firmware code know that the core has been modified:

`~/.platformio/packages/framework-arduinoteensy/cores/usb_rawhid/core_id.h`:

```
#define CORE_KOMAHUB_OK
```
