# Shop Name
Shop Name is a mod made with BepInEx for use with the Potion Craft game. It allows you to name your shop!

### Automated Installation (Recommended)

- Download and install Thunderstore Mod Manager or r2modman.
    - Click Install with Mod Manager button on top of the page.
    - Run the game via the mod manager.

### Manual Installation
(If you have BepInEx installed already you can skip this first step)
- Download the latest BepInEx 5 release from [here](https://github.com/BepInEx/BepInEx/releases)
    - Note: You need the `BepInEx_64` version for use with Potion Craft
    - The contents of the `BepInEx` zip should be extracted to your `PotionCraft` steam directory
    - If properly installed, you should see a `winhttp.dll` file and `BepInEx` folder alongside your `Potion Craft.exe`
    - For a more detailed installation, you can read the installation for `BepInEx` [here](https://docs.bepinex.dev/articles/user_guide/installation/index.html)
    - Once installed `run the game once` to generate the config files and folders. Then close the game

- Download the latest release from the [releases page](https://github.com/MattDeDuck/ShopName/releases)
    - Unzip the folder and head to your Potion Craft game directory
    - Copy the folder containing the `ShopName.dll`, `shopname.json` and `shopbgBIG.png` files into `Potion Craft/BepInEx/plugins/`
    - Run the game


### How to name your alchemy shop
- Open up `shopname.json` in any text editor or IDE of your choice
- Change the value of `Shop Name` to whatever you wish to call your shop

```json
{
  "Shop Name": "Your Alchemy Shop Name Here"
}
```


### Image

![Shop name](https://github.com/MattDeDuck/ShopName/blob/master/images/screen1.png?raw=true)