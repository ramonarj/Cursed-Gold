var yourCursor : Texture2D;  // Your cursor texture
var cursorSizeX : int = 16;  // Your cursor size x
var cursorSizeY : int = 16;  // Your cursor size y
 
function Start()
{
    Cursor.visible = false;
}
 
function OnGUI()
{
    GUI.DrawTexture (Rect(Event.current.mousePosition.x-cursorSizeX/2, Event.current.mousePosition.y-cursorSizeY/2, cursorSizeX, cursorSizeY), yourCursor);
}