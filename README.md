# Tringle
A C# and OpenTK game engine (currently under development).

<h2>Building</h2>

<h3>Windows or Mac</h3>

<ol>
<li>Open in Visual Studio Community/Enterprise/Professional (2022 recommended)
<p>NOTE: if on Mac, go to Global.cs and set the static variable, MAC, to true</p>
</li>
<li>Build</li>
</ol>

<h2>Features</h2>

<ol>
<li>Easy mesh and sprite rendering</li>
<li>Easily implement multiple cameras using the CameraManager class</li>
<li>Easily use a texture atlas with your meshes using the TextureAtlas class</li>
<li>Easy texture/shader loading and indexing</li>
<li>Basic entity component system (ECS)</li>
<li>AABB component for use in rigidbody physics</li>
</ol>

<h2>Examples</h2>

The examples folder contains the game files for a version of Tetris, Tetringle, that I created. I intend to add more examples in the future to showcase the features of the Tringle game engine.
Tetringle uses the original right-hand rotation system (used in the NES version). Lines clear when a full line is achieved. If a piece is both grounded and above the height-limit, the game ends.

Use the right and left arrow to move horizontally.
Use the up arrow to rotate.

<h3>Screenshots</h3>

Figure 1: Main game

<img width="412" alt="playing" src="https://user-images.githubusercontent.com/105574500/190833302-34b00258-fd2f-4091-88dc-15dd54e3dec8.png">

Figure 2: Paused

<img width="412" alt="paused" src="https://user-images.githubusercontent.com/105574500/190833306-36d538cc-219e-4830-8985-47c23eb74294.png">

Figure 3: Game over

<img width="412" alt="gameover" src="https://user-images.githubusercontent.com/105574500/190833310-57f99ff5-ebb4-4df1-8723-7a64529b3792.png">

<h2>Features in development</h2>

<ol>
<li>Text</li>
<li>Audio</li>
<li>Complete (rigidbody) physics system</li>
</ol>

<h2>Future features/plans (hopes)</h2>

These are overly ambitious plans for the long-term development of Tringle.

<ol>
<li>Add support for other graphics specifications, specifically Vulkan and DirectX (this would require rewriting the core codebase in C++)</li>
<li>Deferred rendering and shadows</li>
<li>Fluid and soft body physics</li>
<li>Web/iOS/Android</li>
</ol>
