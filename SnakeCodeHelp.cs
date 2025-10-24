// Here are the fields we'll add to your snake monobehavior class to keep track of the body
//
public GameObject bodySegmentPrefab;          // This is the prefab in unity that is like the sprite for each body segment
private GameObject[] bodySegments;             // This is an array of Transforms. We'll use this to record the position of each body segment
private int snakeLegnth = 0;                // This trans the length of the snake (how many body segments it has). It starts with 0 meaning it only has a head

// --------------------------------------------------------------------------------------
//  Now let's talk about the logic
//
//  We're going to insert the following code into your current project:
//     1. Code that runs at the start to initialize the snake body
//     2. Code that creates a new body segment every time you run into an apple
//     3. Code that updates the position of each body segment every time the snake moves
// --------------------------------------------------------------------------------------


// --------------------------------------------------------------------------------------
//  Part 1 - code that runs at the start to initialize the snake body
//  
//  Below, I'm going to describe what the code needs to do without providing you the code.
//  When we get together, I'll help you write the code that does what the code needs to do
// --------------------------------------------------------------------------------------
During your Start() function, you need to 
1. Allocate your array of bodySegments
2. You need to go through each bodySegment in the array and 
    2a. Instantiate the segment so that the unity engine knows it exists
    2b. Set the segment to not be active
    3c. Record the newly instantiated segment in your array of segments so that you can use it to record the segment's position later

// --------------------------------------------------------------------------------------
//  Part 2 - code that creates a new body segment every time you run into an apple
//  
//  Below, I'm going to describe what the code needs to do without providing you the code.
//  When we get together, I'll help you write the code that does what the code needs to do
// --------------------------------------------------------------------------------------
First, we will add a new method called "AddBodySegment". It will do everything we need whenever we add a body segment.
void AddBodySegment()
{
  // We will add code to this new method that does the following:
  1. Determines the location to spawn the new segment. It will eather be the position of the head (if there are no body segments already)
     or it will be the current position of the last body segment (if there are body segments already). So we need to have an if statement 
     for if the SnakeLength > 0. If you want to know where the last body segment of the snake currently is, it will be something in 
     bodySegments[SnakeLength - 1].position. The new body segment that we are adding will be in bodySegments[SnakeLength].position
  2. Spawn the new segment. We can do this by just setting the new segment in the array to active.
  3. Increase the SnakeLength because the snake is longer now
    
}

// Now that we have an AddBodySegment method, the rest is easy. You already have a spot in your code where you handle what happens 
// when the snake eats an apple. We're going to go to that spot in your code an add additional code that does this:
1. Calls the new AddBodySegment method

// --------------------------------------------------------------------------------------
//  Part 3 - code updates the position of each body segment everytime the snake moves
//  
//  Below, I'm going to describe what the code needs to do without providing you the code.
//  When we get together, I'll help you write the code that does what the code needs to do
// --------------------------------------------------------------------------------------

// This is the trickiest part but it's not that bad once you get the hang of it
// You already have code that makes the head move. We need to go to that part of your code 
// We'll define a new variable here to record the "previous position". That way, each body segment
// can be updated so that it's position is the previous position of the segment in front of it.

Vector3 previousPosition = transform.position;   // This code defines our variable to record the previous position. transform.position is the snake head position.

// And add some new code that uses a for loop to update each body section
for (int i = 0; i < snakeLength; i++)
{
  // The code inside this loop will run once for every segment in the body starting at the first segment and working to the last
  1. Add code to record what the position of the current segment is in a temporary variable
  2. Update the position of the current segment to be the previousPosition. This will make it move to the spot where the segment in front of it was
  3. Update the previousPosition to be the temporary variable you saved in #1
}

  
