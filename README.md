# Conception!

<!-- vscode-markdown-toc -->
* 1. [Setting up the project](#Settinguptheproject-0)
* 2. [Setting up your editor](#Settingupyoureditor-1)
	* 2.1. [Visual Studio (Windows)](#VisualStudioWindows-2)
	* 2.2. [Visual Studio Code](#VisualStudioCode-3)
		* 2.2.1. [Install Mono](#InstallMono-4)
		* 2.2.2. [Set up VSCode](#SetupVSCode-5)
		* 2.2.3. [Set up Unity](#SetupUnity-6)

<!-- /vscode-markdown-toc -->

##  1. <a name='Settinguptheproject-0'></a>Setting up the project

1. Install Unity
2. Clone this repo somewhere sensible
3. Tell Unity to Open Project, and point it to the `Conception` directory
4. If you get messages about non-matching editors or reimporting (I'm using an older version of Unity), say yes to everything
5. You're done!

##  2. <a name='Settingupyoureditor-1'></a>Setting up your editor

Unity is bundled with MonoDevelop, which is terrible. Instead, I recommend Visual Studio or Visual Studio Code (my preference). Really you can use any editor you like, but using one of these will give you nice things like access to the debugger, outputting to the console, autocompletion...

To change the default editor in Unity, go to `Preferences > External Tools`, and change the `External Script Editor`.

###  2.1. <a name='VisualStudioWindows-2'></a>Visual Studio (Windows)

I won't go into this, because it's pretty straightforward — support is built into Unity and these days it pretty much sets itself up.

###  2.2. <a name='VisualStudioCode-3'></a>Visual Studio Code

This is a bit more complicated. You need to make some changes both to VSCode and to the Unity project to get this working.

####  2.2.1. <a name='InstallMono-4'></a>Install Mono
Mono is the C# framework used by Unity, and you'll need to install it independently before VSCode will be able to check your code.

1. Go to <http://www.mono-project.com/download/> and download and install Mono

####  2.2.2. <a name='SetupVSCode-5'></a>Set up VSCode
1. Install VSCode
2. Open the extensions panel (bottom of the strip of icons down the left)
3. If you have an extension named `C#` listed, uninstall it
4. Search for and install the extension `Legacy C# Support`
5. Restart VSCode

####  2.2.3. <a name='SetupUnity-6'></a>Set up Unity
1. Make a subdirectory in the Unity project's Assets folder called Utilities (not necessary, but I have added this to .gitignore)
2. Clone <https://github.com/dotBunny/VSCode> into the above directory
3. Go to `Preferences > VSCode` and set `Enable Integration` and `Use Unity Debugger`, and click on `Install Unity Debugger`
4. Restart VSCode once it's done