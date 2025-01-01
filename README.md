# Shop Name
Shop Name is a mod made with BepInEx for use with the Potion Craft game. It allows you to name your shop.

### BepInEx
- Download the latest BepInEx release from [here](https://github.com/BepInEx/BepInEx/releases)
Note: You need the `BepInEx_64` version for use with Potion Craft
- You can read the installation for BepInEx [here](https://docs.bepinex.dev/articles/user_guide/installation/index.html)
- Once installed run once to generate the config files and folders

### Shop Name installation
- Download the latest version from the [releases page](https://github.com/MattDeDuck/ShopName/releases)
- Unzip the folder
- Copy the folder into `Potion Craft/BepInEx/plugins/`
- Run the game

### How to name your shop
- Open up `shopname.json` in any text editor or IDE of your choice
- Change the value of `Shop Name` to whatever you want to call your shop

```json
{
  "Shop Name": "Your Shop Name Here"
}

```

You can even put icons in your shop name to make it look more stylish!

Take a look at what icons you can use with the [Icons List](https://github.com/MattDeDuck/ShopName/blob/master/icons.txt)

An example is this below:

```json
{
  "Shop Name": "<sprite=\"IconsAtlas\" name=\"Juggler\"> Magical Mystical Mixmasters <sprite=\"IconsAtlas\" name=\"Juggler\">"
}

```

### Example Image
![Shop Name](https://github.com/MattDeDuck/ShopName/blob/master/images/examplescreen.png?raw=true)


### How to update the name
- Open up `shopname.json` in any text editor or IDE of your choice
- Change the value of `Shop Name` to whatever you want to call your shop
- Hit the blue refresh arrow button to the left of the name to update the text!