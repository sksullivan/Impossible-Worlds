# Impossible-Worlds
VR Demo for mystery and optical illusions.

Roadmap:

- [ ] Proof-of-concept work
  - [ ] Test teleporting in unity
  - [ ] Map element decomposition for movement (do penrose steps move when you’re not looking at them? if so, they can’t be static like the rest of the map)
  - [ ] Test Blender -> Unity pipeline again (scaling, navigability with VR)
  - [ ] Specifically test interior collisions for mesh colliders from blender
- [ ] Survey any real-life settings
  - [ ] Collect photos/list of desired textures
  - [ ] Decide on measurements (how high stairs are, ceiling heights, etc)
  - [ ] Compile rough list of what entities (small, high-poly map detail items) are needed
- [ ] Create final floorplans
  - [ ] My apt bldg
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

Day 1 Summary:

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
