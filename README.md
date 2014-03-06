#L-System Generator for Unity

This is my first attempt at creating a Unity wizard for the creation of objects on screen.

I decided to go with creating L-Systems since I thought they look pretty cool and I wanted to learn a little bit of C#
while I was at it.

##How to use

Once you have the script files in whatever project you want L-Systems to appear in, just go through Unity's menu system:
GameObject->Create Other...->L-System... and follow the onscreen instructions from the wizard to create your very own system on screen.

##Issues

* LSystemPainter is not completed yet, only outputs the L-System using Debug.DrawLine.
* Have to add in support for multiple variables, right now only uses the variable 'F'.

##Future

* Would like to allow this system to make 3D L-Systems in the future.

##Example

Check out [L-System](http://www.stefangawrys.com/LSystem/LSystem.html) for an example using Unity Web Player, added a little bit more code for the L-System to display on the web.