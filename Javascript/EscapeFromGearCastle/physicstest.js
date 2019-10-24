"use strict"


let scale = 30;//draw scale

var b2Vec2 = Box2D.Common.Math.b2Vec2,                  //all these are name spaces for shortcut when use functions in Box2DJS 
    b2AABB = Box2D.Collision.b2AABB,
	b2BodyDef = Box2D.Dynamics.b2BodyDef,
	b2Body = Box2D.Dynamics.b2Body,
	b2FixtureDef = Box2D.Dynamics.b2FixtureDef,
	b2Fixture = Box2D.Dynamics.b2Fixture,
	b2World = Box2D.Dynamics.b2World,
	b2PolygonShape = Box2D.Collision.Shapes.b2PolygonShape,
    b2CircleShape = Box2D.Collision.Shapes.b2CircleShape,
	b2DebugDraw = Box2D.Dynamics.b2DebugDraw,
    b2Joint = Box2D.Dynamics.Joints.b2Joint,
    b2JointDef = Box2D.Dynamics.Joints.b2JointDef,
    b2DistanceJointDef = Box2D.Dynamics.Joints.b2DistanceJointDef,
    b2WeldJointDef = Box2D.Dynamics.Joints.b2WeldJointDef,
    b2RevoluteJointDef = Box2D.Dynamics.Joints.b2RevoluteJointDef,
    b2Contact = Box2D.Dynamics.Contacts.b2Contact,
    b2ContactListener = Box2D.Dynamics.b2ContactListener,
    b2Transform = Box2D.Common.Math.b2Transform,
    b2Mat22 = Box2D.Common.Math.b2Mat22,
    b2Controller = Box2D.Dynamics.Controllers.b2Controller;
    

function createWorld(){                                 //create physcs world
    
    let gravity=new b2Vec2(0,60);
    let doSleep = true;    
    let world = new b2World(gravity, doSleep);
    return world;
}

function createBox(world,type,x,y,width,height,density,friction,restitution){   //for creating box body
    let bodyDef = new b2BodyDef();
    bodyDef.position = new b2Vec2(x/scale,y/scale);
    bodyDef.type=type;
    bodyDef.allowSleep=false;
    
    let fixtureDef = new b2FixtureDef();
    fixtureDef.density=density;
    fixtureDef.friction=friction;
    fixtureDef.restitution=restitution;
    let shape = new b2PolygonShape();
    shape.SetAsBox(width/2/scale,height/2/scale);
    fixtureDef.shape=shape;
    
    let boxBody=world.CreateBody(bodyDef);
    boxBody.CreateFixture(fixtureDef);
    
    return boxBody
}

function createBall(world,type, x, y, r,density,friction,restitution){   //for creating circle body
    let bodyDef = new b2BodyDef();
    bodyDef.position = new b2Vec2(x/scale,y/scale);
    bodyDef.type=type;
    bodyDef.allowSleep=true;
    
    let fixtureDef = new b2FixtureDef();
    fixtureDef.density=density;
    fixtureDef.friction=friction;
    fixtureDef.restitution=restitution;
    let shape = new b2CircleShape(r/2/scale);
    fixtureDef.shape=shape;
    
    let ballBody=world.CreateBody(bodyDef);
    ballBody.CreateFixture(fixtureDef);
    
    return ballBody
}


function setDebugDraw(world){                       //for drawing debug graphics of physic world
        let debugDraw = new b2DebugDraw();
        debugDraw.SetSprite(debugContext);
        debugDraw.SetDrawScale(30);
		debugDraw.SetFillAlpha(0.1);
		debugDraw.SetLineThickness(20);
		debugDraw.SetFlags(b2DebugDraw.e_shapeBit | b2DebugDraw.e_jointBit);

		world.SetDebugDraw(debugDraw);
        
        return debugDraw
}

