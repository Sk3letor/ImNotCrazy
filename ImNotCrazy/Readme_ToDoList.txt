----------------To do list-------------------------

jprajzne: 

first step:
1. Create Empty gameobject ( GameObject - Creare Empty ) 
2. Optional -  Add Character Controller ( Component - Physics - Character Controller )
3. Add 3 Scripts by drag dropping ( obj must be selected ) to inspector or to obj self in scene.
FirstPerson_Movement, DragDrop and CursorManager.
4. Uncheck Cursor Manager script.
5. Make sure obj is selected and add gameobject child  ( GameObject - Create Empty Child )
6. Name player's obj to player or something similiar and child obj to "camera holder" or something similiar.
7. Drag those objs into FirsPerson_Movement's places "Player Camera and Camera Holder"
8. Make new layer called " CrouchCollision " ( Select any obj and see Layer top right in inspector).
and select that in FirstPerson movement's " Crouch Collision " 
9. DragDrop Camera to Drag Drop script's " Camera Obj ".
10. Now make plane as a floor.
11. Create Cube gameobject and give Rigidbody for it ( Component - Physics - Rigidbody )
12. Make new tag in inspector called "pick" and select it for cube obj. 
13. Test and learn. 