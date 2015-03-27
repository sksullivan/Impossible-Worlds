# Impossible-Worlds
VR Demo for mystery and optical illusions.

Roadmap:

- [ ] Proof-of-concept work
  - [ ] Test teleporting in unity
  - [x] Map element decomposition for movement (do penrose steps move when you’re not looking at them? if so, they can’t be static like the rest of the map)
  - [x] Test Blender -> Unity pipeline again (scaling, navigability with VR)
  - [x] Specifically test interior collisions for mesh colliders from blender
- [ ] Survey any real-life settings
  - [ ] Collect photos/list of desired textures
  - [ ] Decide on measurements (how high stairs are, ceiling heights, etc)
  - [ ] Compile rough list of what entities (small, high-poly map detail items) are needed
- [ ] Create final floorplans
  - [x] My apt bldg
  - [ ] Atrium/static gallery, video gallery (modeled after AIC)
  - [ ] Room of other worlds, including text projection centerpiece
  - [ ] Escher’s Waterfall
  - [ ] Penrose Steps
  - [ ] Nate’s Creations (caves, dark water)
- [ ] Design look of structures
- [ ] Model/Texture structures
- [ ] Model/Texture entities
  - [ ] Import/make little map things (plants, mailboxes, papers, coffee mugs, etc)
  - [ ] Make optical illusion structures
  - [ ] Add textures to structures for optical illusions gallery
- [ ] Add Lights
- [ ] Add interactivity
  - [ ] Doors opening
  - [ ] People on penrose steps?
  - [ ] Maybe some wind or other atmospherics
- [ ] Add sounds

Day 1 Notes:

Mostly did work on the Blender -> Unity pipeline. Determined that the best way to model/do textures is to use Unity 4.7+ (not 4.6, which is on the lab machines) and Blender 2.74. The process is as follows:

Blender Part  
1. Model geometry.  
2. Flip normals as needed.  
2. Create materials for each different material on your model.  
3. Create a new texture for each material.  
4. Set texture as Image -> Open desired image -> play with tiling settings.  
5. Select all of mdoel in edit mode, then UV unwrap model.  
6. Create new (blank) UV Map image at 4096 by 4096 px.  
7. Switch to object mode, select object, then hit "bake" under render menu.  
8. Save generated UV image.  
9. Save model as .blend. 

Unity Part  
10. Import .blend into Unity.  
11. Drag generated UV image into unity assets.  
12. Set import settings to use Point (not bilinear) filtering, and set max size to 4096 by 4096 (uncompressed) true color.  
13. Finally set UV image as texture for the various Material__Bake materials.

It's a pretty involved process, but produces pretty good looking results if you do it properly. UV unwrapping will be really useful for certain illusions. This does occasionally produce minor visible seams, strangely, but that could be an issue in Blender where culling isn't enabled, bad normals, etc. Not too big of a deal for now. Next steps are to work in entities, and to nail down how we're going to pull off the unity "tricks" we need for this project, i.e. teleporting, moving objects out of view, and doors.


Day 2 Notes:

- ALWAYS, always check your normals. So many issues flipping things back and forth between Blender and Unity.
- Best way to UV unrap is with "Smart UV Project."
- To reimport something in Unity, you can't just hit "reimport," you must manually delete and re-add it to assets.
- Blender models can just have multiple objects in them, which map to different objects and collision meshes in Unity. Good for the moving stairs problem.
- Stairs, while modeled with blocks, must have sloped box colliders unfortunately.
- Unity handles array modifiers in Blender automatically. Woohoo! Make sure that you normalize rotations and scaling for the object to which you apply an array modifier with "Apply - Scale & Rotation" when using object offsets.

Today's main focus involved the portal effect needed for the juxtaposed worlds. This, unfortunately, has turned out to be very complex. I first tried the internet's suggestion to use a modified mirror to render a remote camera's output on a plane via a shader. I couldn't get this to work at all, and furthermore, depth perception was not addressed by this approach. Next, I used render textures to place what a camera sees onto a plane via a material, which suffered from the same problem as the first method. Finally, my last attempt involved the following:

1. Place a camera facing the subject of the portal.  
2. Place another camera facing the subject, the IPD to the right of the first camera.  
3. Send the output of each camera to two render textures, left and right.
4. Place two planes in the same location, for the portal.
5. Texture one plane with the left render texture and the other with the right render texture.
6. Make each plane visible only to the proper eye by moving the right onto a "RIGHT EYE ONLY" layer in Unity, and the left onto a "LEFT EYE ONLY" layer.
7. Unckeck the appropriate layer from the list of displayed layers (culling, I think?) on the right and left eye anchors in the OVRPlayerController.

Finally, after making the surrogate camera pair's motion mirror the OVRPlayerController, a correct-depth result was achieved. This portal, however, results in the portal's contents being displayed at the wrong height in the visual field. It just looks wrong, somehow. Tomorrow I will attempt to correct this by making the aforementioned planes always tangent to the ray of the player's current looking direction.

Day 3 Notes:

Haven't tried the perpendicular render texture approach yet. Tried another method for world juxtaposition where normals are flipped (making certain surfaces visiable and invisible) based on player location. While this would work, in practice it's simply way to complicated to pull off for the complexity we're going for. Will try some more camera techniques.

So at the end of the day it looks like the best course of action is to avoid the "looking through a portal" scenario. It can probably be pulled off with correct depth, parallax, etc, but not in this week. Instead, I've devised a way to fake the juxtaposed worlds trick with some simple trigonometry but complex control logic. TIL game performance takes a massive hit in Unity if you add too many debug statements.
