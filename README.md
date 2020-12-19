# christmas-wallpaper
Adds a Christmas tree to desktop wallpaper, and adds a new item to the tree on each day leading up to Christmas.

### What does it do?
On the the 15th December, the program will add a Christmas tree to the desktop wallpaper:

![Wallpaper with only a Christmas tree, on the 15th December](https://github.com/AlexFF000/christmas-wallpaper/blob/screenshots/Screenshots/firstDay.PNG)

Every day after the 15th, it will randomly choose a new item to add to the tree (e.g. baubles, lights, presents):

![Wallpaper with a Christmas tree and baubles](https://github.com/AlexFF000/christmas-wallpaper/blob/screenshots/Screenshots/thirdDay.PNG)

Finally, on the 25th the tree will be complete:

![Wallpaper with the complete Christmas tree, including baubles, presents, lights, and a star](https://github.com/AlexFF000/christmas-wallpaper/blob/screenshots/Screenshots/lastDay.PNG) 

On the 26th, it will restore the previous wallpaper.

### Configuration
The settings panel can be used to change the start and end date as desired:

![The settings window, allowing user to change start and end dates and stop the program from running at startup](https://github.com/AlexFF000/christmas-wallpaper/blob/screenshots/Screenshots/settings.PNG)

### Compatibility
The program is compatible with Microsoft Windows 10.  It has not been tested on previous versions of Windows.

### Modification
The program is designed to accommodate users wishing to modify it (for instance, to use it for other holidays).

#### Changing or adding new images
The images are stored by default in the "Images/Resources" folder, although can be stored anywhere on the local file system that the program can access.  **The "Images/State" folder (which is only created on first run) is for the program's internal use, and its contents should not be modified.**

To add new images (or replace existing ones), simply add them to the "Images" dictionary in config.json.  Items in the dictionary should be in the following format: "<image name>":"<image path>".
The config.json file must also specify the base image, which is the image added on the first day that the others are placed on top of (by default, this is the tree).  This should be specified as follows:
"BaseImage":"<name of image (must be the same name as used in Images dictionary)>".

#### Changing the start and end date
The start and end date can be changed either through the settings GUI form, or by manually changing the values in config.json. **Please note that the year is ignored, so the program does not support start and end dates that fall on different years.**

If there are more days between the start and end date than there are images, then no more modifications will be made once all images have been used.  Likewise, if there are more images than days, then not all images will be used.

**Note that the state.json file is for the program's internal use, and should not be modified"**

### Image Credits
The default tree, star, lights, and baubles are all taken from an image by MTDEWBUNNY
Available at: https://openclipart.org/detail/173440/christmas-tree-with-lights

The desktop wallpaper image used in the screenshots for this readme document is Duman Community Park by Ron Shawley.
Licensed under Creative Commons 3.0.
Availabe at: https://commons.wikimedia.org/wiki/File:Duman_Community_Park_-_panoramio_(6).jpg
