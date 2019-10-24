let isGround=0;
let characterDirection; // -1 for left, 1 for right;
let characterStatus=0;  // 0 for idle,1 for run ,2 for jump, 3 for rotating, 4 for dead 
let canJump=1; //1 for can 0 for cannot,for ensure impulse/force can only apply once in one jump
let canRotate=1;//1 for can o for cannot;
let rotationDirection; // 1 for clockwise, -1 for counterclockwise;
let interact=false;// for check if interact with objects;


class character{                        //class of character
    
    constructor(gameStage,world,x,y,width,height,density,friction,restitution,isCollide,speed,jumpforce){
        this.gameStage=gameStage;
        this.world=world;
        this.x=x;
        this.y=y;
        this.width=width;
        this.height=height;
        this.density=density;
        this.friction=friction;
        this.restitution=restitution;
        this.isCollide=isCollide;
        this.speed=speed;
        this.jumpforce=jumpforce;
        this.characterBody;
        this.characterSprite;
      
    }
    

    createCharacterBody(){              //create the character's physics body by Box2DJS.
    let bodyDef2 = new b2BodyDef();                           //it's the circle body work as a wheel.      
    bodyDef2.position = new b2Vec2(this.x/scale,this.y/scale);
    bodyDef2.type=2;
    bodyDef2.allowSleep=false;
        
    let fixtureDef2 = new b2FixtureDef();
    fixtureDef2.density=this.density;
    fixtureDef2.friction=this.friction;
    fixtureDef2.restitution=this.restitution;
    let shape2 = new b2CircleShape(this.width/2/scale);
    fixtureDef2.shape=shape2;
    fixtureDef2.userData="wheel";
    let ballBody=world.CreateBody(bodyDef2);
    ballBody.CreateFixture(fixtureDef2);
        
    let bodyDef1 = new b2BodyDef();
    bodyDef1.position = new b2Vec2(this.x/scale,this.y/scale-(this.height-8)/2/scale);
    bodyDef1.type=2;
    bodyDef1.allowSleep=false;
    
    let fixtureDef1 = new b2FixtureDef();                           //it's the box body work as the main body
    fixtureDef1.density=this.density/2;
    fixtureDef1.friction=this.friction;
    fixtureDef1.restitution=this.restitution;
    let shape1 = new b2PolygonShape();
    shape1.SetAsBox((this.width-6)/2/scale,(this.height-this.width/2)/2/scale);
    fixtureDef1.shape=shape1;
    fixtureDef1.userData="characterbody";
    let boxBody=world.CreateBody(bodyDef1);
    boxBody.CreateFixture(fixtureDef1);
    boxBody.SetFixedRotation(true);


    
        let vertices=[];
    vertices[0]=new b2Vec2(-this.width/4/scale,(this.height/2+2)/scale);
    vertices[1]=new b2Vec2(this.width/4/scale,(this.height/2+2)/scale);
    vertices[2]=new b2Vec2(this.width/4/scale,(this.height/2+8)/scale);
    vertices[3]=new b2Vec2(-this.width/4/scale,(this.height/2+8)/scale);
    
    let fixtureDefmini = new b2FixtureDef();                      //it's a sensor fixture on main body for ground check.
    fixtureDefmini.density=this.density/2;
    fixtureDefmini.friction=this.friction;
    fixtureDefmini.restitution=this.restitution;
    let shapemini = new b2PolygonShape();
    shapemini.SetAsArray(vertices,4);
    fixtureDefmini.shape=shapemini;
    fixtureDefmini.isSensor=true;
    
    let miniFixture=boxBody.CreateFixture(fixtureDefmini);
    miniFixture.SetUserData("groundCheckSensor");
    
    let  revoluteDef = new b2RevoluteJointDef();                    //it's a revolution joint which makes the circle body works like a wheel
    revoluteDef.collideConnected=this.isCollide;
    revoluteDef.bodyA=boxBody;
    revoluteDef.bodyB=ballBody;
    revoluteDef.enableMotor=true;
    revoluteDef.localAnchorA.Set(0,this.width/2/scale);
    revoluteDef.localAnchorB.Set(0,0);
    
    let revoluteJoint = world.CreateJoint(revoluteDef);
    revoluteJoint.SetUserData(revoluteJoint);
    return revoluteJoint
}
    
    createCharacterSprite(){                    //create the character's sprites by createJS
        let spriteData = {                                  //create sprites sheets
    images:["sprites/testsprites.png"],
    frames:{width:50, height:37,count:72,regX:25,regY:19},
    animations:{
    idle:[0,3,"idle",0.1],                                  //setup animations I need
    run:[8,13,"run",0.2],
    jump:[14,21,"fall",0.3],
    fall:[22,23,"fall",0.2],
    dead:[4,7,"dead",0.1]
}
}
let characterSpriteSheet = new createjs.SpriteSheet(spriteData);
let characterSprite = new createjs.Sprite(characterSpriteSheet,"idle")

characterSprite.x=this.x;
characterSprite.y=this.y;
this.gameStage.addChild(characterSprite);
return characterSprite
    }
    
    createCharacter(){                                              //combine two create function
        this.characterBody=this.createCharacterBody();
        this.characterSprite=this.createCharacterSprite();
    }
    
    characterSynchronize(){                                         //Synchronize the position of character's physics body and sprite
     let characterMainBody=this.characterBody.GetBodyA();
     let characterSubBody=this.characterBody.GetBodyB();
     let originPosition=characterSubBody.GetPosition();
     let bodyPosition=new b2Vec2(originPosition.x,originPosition.y-(this.height-8)/2/scale);
     characterMainBody.SetPosition(bodyPosition);   // fix the distance of two bodies
     this.characterSprite.x=bodyPosition.x*scale;
     this.characterSprite.y=bodyPosition.y*scale+2;
     this.characterSprite.rotation=characterMainBody.m_rotation/Math.PI*180;
    }
    
    characterMove(){                                                      //character movement function 
        
        let checkbody=this.characterBody.GetBodyA();
        let checkbodyVelocity=checkbody.GetLinearVelocity();
        let animationStatus=this.characterSprite.currentAnimation;
    if(characterStatus==1) { 
        this.characterBody.SetMotorSpeed(characterDirection*this.speed);
        this.characterBody.SetMaxMotorTorque(500);  
        this.characterSprite.set({scaleX:characterDirection});
       if(animationStatus != "jump" && animationStatus != "run" && isGround==1){
            this.characterSprite.gotoAndPlay("run");
        }
    }
    if(characterStatus==2 && isGround==1 && canJump==1 && characterStatus!=3){
        canJump=0;
        checkbody.ApplyImpulse(new b2Vec2(0,-this.jumpforce),checkbody.GetPosition());
        isGround=0;
        this.characterSprite.gotoAndPlay("jump");
        setTimeout(timer,500);
        if(this.characterBody.GetMotorSpeed()==0){
        characterStatus=0;
        }else {
            characterStatus=1;          
        }
    }
     if(characterStatus==0 && isGround==1 && characterStatus!=3){
         this.characterBody.SetMotorSpeed(0);
         if(this.characterSprite.currentAnimation!="idle"){
         this.characterSprite.gotoAndPlay("idle");
        }
     }
     if(isGround==1 && (this.characterSprite.currentAnimation=="fall" || this.characterSprite.currentAnimation=="jump") && characterStatus!=3){
         if(this.characterBody.GetMotorSpeed()==0){
         this.characterSprite.gotoAndPlay("idle");
         }else if(this.characterBody.GetMotorSpeed()!=0){
          this.characterSprite.gotoAndPlay("run");   
         }
     }
        
     if(isGround==0 && this.characterSprite.currentAnimation!="jump" && this.characterSprite.currentAnimation!="fall"){
         this.characterSprite.gotoAndPlay("fall");
     }
        
     if(characterStatus==3 && canRotate==1){
         canRotate=0;
     let characterboxBody=this.characterBody.GetBodyA();
     let charactercircleBody=this.characterBody.GetBodyB();
     let originPos=charactercircleBody.GetWorldPoint(new b2Vec2(0,0));
     let boxfixture=characterboxBody.GetFixtureList();
     let circlefixture=charactercircleBody.GetFixtureList();
        boxfixture.SetSensor(true); 
        circlefixture.SetSensor(true);
         
         
         charactercircleBody.SetPosition(new b2Vec2(originPos.x,originPos.y-20/scale));
         characterboxBody.SetType(0);

         isGround=0;
         
         if(animationStatus!="fall"){
             this.characterSprite.gotoAndPlay("fall");
         }
         
         for(let i=0;i<objectsarray.length;i++){
             if(objectsarray[i].type==5||objectsarray[i].type==6||objectsarray[i].type==7||objectsarray[i].type==8){
                 objectsarray[i].body.SetType(1);
             }
         }
    
         setTimeout(rotateAll(this.characterSprite.x,this.characterSprite.y+4),300);


        setTimeout(timer2,2000);
        
         
                function timer2(){
            if(boxfixture.m_userData!="groundCheckSensor"){        
            boxfixture.SetSensor(false); }
           circlefixture.SetSensor(false);
            characterboxBody.SetType(2);
            characterStatus=0;
            canRotate=1;
        }

     }
        
        function timer(){
            canJump=1;
        }
    if(characterStatus==4&&this.currentAnimation!="dead"){
             this.characterSprite.gotoAndPlay("dead");
    }
}
    statusChange(e){                                            //character status switch for key down
        if(characterStatus!=3){
        /*run*/
    if(event.key=="a"  && characterStatus !=1){
        characterStatus=1;
        characterDirection=-1;

    }else if(event.key=="d" && characterStatus !=1){
        characterStatus=1;
        characterDirection=1;
    }
        /*jump*/
    if(event.key=="w" && characterStatus !=2){
        characterStatus=2;
    }
        /*rotate level*/
    if(event.key=="k"){ //clockwise

        characterStatus=3;
        rotationDirection=1;
    } else if(event.key=="j") {  //counterclockwise

        characterStatus=3;
        rotationDirection=-1;
    } 
        }
}

    statusBack(e){                                                  //character status switch for key up
        if(characterStatus!=3){
    if(event.key=="a" || event.key=="d" && characterStatus ==1){
        characterStatus=0;
    }
    if(event.key=="w" && characterStatus ==2 && isGround==0){
        characterStatus=0;
    }
        }
} 

    dead(){                                                         //how character perform when died
     let characterboxBody=this.characterBody.GetBodyA();
     let charactercircleBody=this.characterBody.GetBodyB();
        characterboxBody.SetType(0);
        charactercircleBody.SetType(0);
        characterStatus=4;
    }
    
}


class collisionCheckListener extends b2ContactListener{                // all kind of collision detection made by Box2DJS
    BeginContact(contact){
        super.BeginContact();                                           //for ground check.
        let contactUserDataA = contact.GetFixtureA().GetUserData();
        let contactUserDataB = contact.GetFixtureB().GetUserData();
        if(((contactUserDataA=="groundCheckSensor"&&(contactUserDataB=="ground"||contactUserDataB=="heavy1"||contactUserDataB=="heavy2"||contactUserDataB=="heavy3"||contactUserDataB=="heavy4"))||(contactUserDataB=="groundCheckSensor"&&(contactUserDataB=="ground"||contactUserDataA=="heavy1"||contactUserDataA=="heavy2"||contactUserDataA=="heavy3"||contactUserDataA=="heavy4")))&& characterStatus!=3){
            isGround=1;
        }
                                                                            //for target door check
        if(((contactUserDataA=="closeddoor" && contactUserDataB=="groundCheckSensor")||(contactUserDataB=="closeddoor" && contactUserDataA=="groundCheckSensor")) && characterStatus!=3){
            for(let i=0;i<tilesarray.length;i++){
               if(tilesarray[i].type==15){
                tilesarray[i].image2.visible=false;
               }
            }
            playerbody.GetBodyA().SetType(0);
            document.removeEventListener("keydown",playerCharacter.statusChange);
            document.removeEventListener("keyup",playerCharacter.statusBack);
            characterStatus=0;
            changeLevel();
        }
                                                                            //for spike collision check
        if((contactUserDataA=="spike" && (contactUserDataB=="wheel"||contactUserDataB=="characterbody"))||(contactUserDataB=="spike" && (contactUserDataA=="wheel"||contactUserDataA=="characterbody"))){
 
            alive=false;
        }
                                                                                //for falling heavy object collision check
        if((contactUserDataA=="heavy1"||contactUserDataA=="heavy2"||contactUserDataA=="heavy3"||contactUserDataA=="heavy4")&& (contactUserDataB=="wheel"||contactUserDataB=="characterbody")){
            if(contact.GetFixtureA().GetBody().GetLinearVelocity().y>5&&characterStatus!=3){
            alive=false;}
        }else if((contactUserDataB=="heavy1"||contactUserDataB=="heavy2"||contactUserDataB=="heavy3"||contactUserDataB=="heavy4") && (contactUserDataA=="wheel"||contactUserDataA=="characterbody")){
            if(contact.GetFixtureB().GetBody().GetLinearVelocity().y>5&&characterStatus!=3){
            alive=false;}
        }
                                                                                //for falling heavy object trigger collision check
        if((contactUserDataA=="TypechangeCheckSensor1"||contactUserDataA=="TypechangeCheckSensor2"||contactUserDataA=="TypechangeCheckSensor3"||contactUserDataA=="TypechangeCheckSensor4" )&& contactUserDataB=="wheel"&&characterStatus!=3&&characterStatus!=2){

            for(let i=0;i<objectsarray.length;i++){
                if((objectsarray[i].body.GetFixtureList().GetUserData()==contactUserDataA)&&objectsarray[i].triggered==false&&objectsarray[i].body.GetContactList()!=null){
                    objectsarray[i].body.SetType(2);
                    objectsarray[i].triggered=true;
                }
                
            }
        }else if((contactUserDataB=="TypechangeCheckSensor1"||contactUserDataB=="TypechangeCheckSensor2"||contactUserDataB=="TypechangeCheckSensor3"||contactUserDataB=="TypechangeCheckSensor4")&& contactUserDataA=="wheel"&&characterStatus!=3&&characterStatus!=2){

                 for(let i=0;i<objectsarray.length;i++){
                if((objectsarray[i].body.GetFixtureList().GetUserData()==ontactUserDataB)&&objectsarray[i].triggered==false){
                    objectsarray[i].body.SetType(2);
                    objectsarray[i].triggered=true;
              }
        }
    }
                                                                                    //for tutorial sign collision check
            if((contactUserDataA=="tutorial" && (contactUserDataB=="wheel"||contactUserDataB=="characterbody"))||(contactUserDataB=="tutorial" && (contactUserDataA=="wheel"||contactUserDataA=="characterbody"))){
            interact=true;
    }
                                                                                    //for collction coin collision check
         if((contactUserDataA=="coin" && (contactUserDataB=="wheel"||contactUserDataB=="characterbody"))||(contactUserDataB=="coin" && (contactUserDataA=="wheel"||contactUserDataA=="characterbody"))){
             let index;
                           for(let i=0;i<objectsarray.length;i++){
                if(objectsarray[i].type==11){
                    let fixture=objectsarray[i].body.GetFixtureList();
                    objectsarray[i].body.DestroyFixture(fixture);
                    objectStage.removeChild(objectsarray[i].image);
                }
            }
            if(coinGet==false){
            currentCoin=currentCoin+1;
        }
            coinGet=true;
    }
}
    EndContact(contact){                                                              //this is end collision check
        super.EndContact();
        let contactUserDataA = contact.GetFixtureA().GetUserData();
        let contactUserDataB = contact.GetFixtureB().GetUserData();
        let playerBodyB=playerbody.GetBodyB();
        let playerLinearVelocity=playerBodyB.GetLinearVelocity();
        let playerAngleDamp=playerBodyB.GetAngularDamping();                                                //for leave from ground check
        if((contactUserDataA=="groundCheckSensor" ||contactUserDataB=="groundCheckSensor")&& playerLinearVelocity.y>1){
            isGround=0;
        }
        
  
                                                                                                            //for leave from tutorial sign check
                    if((contactUserDataA=="tutorial" && (contactUserDataB=="wheel"||contactUserDataB=="characerbody"))||(contactUserDataB=="tutorial" && (contactUserDataA=="wheel"||contactUserDataA=="characerbody"))){
 
            interact=false;
        }
}    
}