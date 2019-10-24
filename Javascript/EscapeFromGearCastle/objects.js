let tutorialText = new createjs.Text("","20px Arial","#ffffff");

let added=false;


class objects{                                              //class of interactive objects
    constructor(stage,type,xpos,ypos,triggerlength=0){
        this.stage=stage;
        this.type=type;
        this.xpos=xpos;
        this.ypos=ypos;
        this.size=32;
        this.image;
        this.body;
        this.image2;
        this.image3;
        this.image4;
        this.triggerlength=triggerlength;
        this.triggered=false;
    }
    
     createObject(){                                                      //create objects according to type, 1~4 are tutorial signs; 5~8 are falling heavy objects; 9 is non-rotation anchor object; 10 is non-rotation object; 11 is collective coin
        if(this.type==1){
        this.image=new createjs.Bitmap("sprites/objects/tutorial.png");
        this.image.x=this.xpos*this.size;
        this.image.y=this.ypos*this.size;
        this.image.regX=this.size/2;
        this.image.regY=this.size/2;
        this.stage.addChild(this.image);
            
        this.body = createBox(world,1,this.xpos*this.size,this.ypos*this.size,this.size,this.size,1,1,0);
        this.body.GetFixtureList().SetUserData("tutorial");
        this.body.GetFixtureList().SetSensor(true);
        }else if(this.type==2){
        this.image=new createjs.Bitmap("sprites/objects/tutorial.png");
        this.image.x=this.xpos*this.size;
        this.image.y=this.ypos*this.size;
        this.image.regX=this.size/2;
        this.image.regY=this.size/2;
        this.stage.addChild(this.image);
            
        this.body = createBox(world,1,this.xpos*this.size,this.ypos*this.size,this.size,this.size,1,1,0);
        this.body.GetFixtureList().SetUserData("tutorial");
        this.body.GetFixtureList().SetSensor(true);
        }else if(this.type==3){
        this.image=new createjs.Bitmap("sprites/objects/tutorial.png");
        this.image.x=this.xpos*this.size;
        this.image.y=this.ypos*this.size;
        this.image.regX=this.size/2;
        this.image.regY=this.size/2;
        this.stage.addChild(this.image);
            
        this.body = createBox(world,1,this.xpos*this.size,this.ypos*this.size,this.size,this.size,1,1,0);
        this.body.GetFixtureList().SetUserData("tutorial");
        this.body.GetFixtureList().SetSensor(true);
        }else if(this.type==4){
        this.image=new createjs.Bitmap("sprites/objects/tutorial.png");
        this.image.x=this.xpos*this.size;
        this.image.y=this.ypos*this.size;
        this.image.regX=this.size/2;
        this.image.regY=this.size/2;
        this.stage.addChild(this.image);
            
        this.body = createBox(world,1,this.xpos*this.size,this.ypos*this.size,this.size,this.size,1,1,0);
        this.body.GetFixtureList().SetUserData("tutorial");
        this.body.GetFixtureList().SetSensor(true);
        }else if(this.type==5){
        this.triggered=false;
        this.image=new createjs.Bitmap("sprites/objects/heavy.png");
        this.image.x=this.xpos*this.size+this.size/2;
        this.image.y=this.ypos*this.size+this.size/2;
        this.image.regX=this.size;
        this.image.regY=this.size;
        this.stage.addChild(this.image);
            
        
        this.body = createBox(world,1,this.xpos*this.size+this.size/2,this.ypos*this.size+this.size/2,this.size*2,this.size*2,8,1,0.005);
        this.body.GetFixtureList().SetUserData("heavy1");
        this.body.GetFixtureList().SetSensor(false);
            
        let vertices=[];
        vertices[0]=new b2Vec2(-this.size/scale,this.size*(this.triggerlength-0.2)/scale);
        vertices[1]=new b2Vec2(this.size/scale,this.size*(this.triggerlength-0.2)/scale);
        vertices[2]=new b2Vec2(this.size/scale,this.size*this.triggerlength/scale);
        vertices[3]=new b2Vec2(-this.size/scale,this.size*this.triggerlength/scale);
    
        let fixtureDefmini = new b2FixtureDef();
        fixtureDefmini.density=0.1;
        fixtureDefmini.friction=0;
        fixtureDefmini.restitution=0;
        let shapemini = new b2PolygonShape();
        shapemini.SetAsArray(vertices,4);
        fixtureDefmini.shape=shapemini;
        fixtureDefmini.isSensor=true;
    
        let miniFixture=this.body.CreateFixture(fixtureDefmini);
        miniFixture.SetUserData("TypechangeCheckSensor1");
        }else if(this.type==6){
        this.triggered=false;
        this.image=new createjs.Bitmap("sprites/objects/heavy.png");
        this.image.x=this.xpos*this.size+this.size/2;
        this.image.y=this.ypos*this.size+this.size/2;
        this.image.regX=this.size;
        this.image.regY=this.size;
        this.stage.addChild(this.image);
            
        
        this.body = createBox(world,1,this.xpos*this.size+this.size/2,this.ypos*this.size+this.size/2,this.size*2,this.size*2,8,1,0.005);
        this.body.GetFixtureList().SetUserData("heavy2");
        this.body.GetFixtureList().SetSensor(false);
            
        let vertices=[];
        vertices[0]=new b2Vec2(-this.size*(this.triggerlength-0.2)/scale,-this.size/scale);
        vertices[1]=new b2Vec2(-this.size*(this.triggerlength-0.2)/scale,this.size/scale);
        vertices[2]=new b2Vec2(-this.size*this.triggerlength/scale,this.size/scale);
        vertices[3]=new b2Vec2(-this.size*this.triggerlength/scale,-this.size/scale);
    
        let fixtureDefmini = new b2FixtureDef();
        fixtureDefmini.density=0.1;
        fixtureDefmini.friction=0;
        fixtureDefmini.restitution=0;
        let shapemini = new b2PolygonShape();
        shapemini.SetAsArray(vertices,4);
        fixtureDefmini.shape=shapemini;
        fixtureDefmini.isSensor=true;
    
        let miniFixture=this.body.CreateFixture(fixtureDefmini);
        miniFixture.SetUserData("TypechangeCheckSensor2");
        }else if(this.type==7){
        this.triggered=false;
        this.image=new createjs.Bitmap("sprites/objects/heavy.png");
        this.image.x=this.xpos*this.size+this.size/2;
        this.image.y=this.ypos*this.size+this.size/2;
        this.image.regX=this.size;
        this.image.regY=this.size;
        this.stage.addChild(this.image);
            
        
        this.body = createBox(world,1,this.xpos*this.size+this.size/2,this.ypos*this.size+this.size/2,this.size*2,this.size*2,8,1,0.005);
        this.body.GetFixtureList().SetUserData("heavy3");
        this.body.GetFixtureList().SetSensor(false);
            
        let vertices=[];
        vertices[0]=new b2Vec2(-this.size/scale,-this.size*(this.triggerlength-0.2)/scale);
        vertices[1]=new b2Vec2(this.size/scale,-this.size*(this.triggerlength-0.2)/scale);
        vertices[2]=new b2Vec2(this.size/scale,-this.size*this.triggerlength/scale);
        vertices[3]=new b2Vec2(-this.size/scale,-this.size*this.triggerlength/scale);
    
        let fixtureDefmini = new b2FixtureDef();
        fixtureDefmini.density=0.1;
        fixtureDefmini.friction=0;
        fixtureDefmini.restitution=0;
        let shapemini = new b2PolygonShape();
        shapemini.SetAsArray(vertices,4);
        fixtureDefmini.shape=shapemini;
        fixtureDefmini.isSensor=true;
    
        let miniFixture=this.body.CreateFixture(fixtureDefmini);
        miniFixture.SetUserData("TypechangeCheckSensor3");
        }else if(this.type==8){
        this.triggered=false;
        this.image=new createjs.Bitmap("sprites/objects/heavy.png");
        this.image.x=this.xpos*this.size+this.size/2;
        this.image.y=this.ypos*this.size+this.size/2;
        this.image.regX=this.size;
        this.image.regY=this.size;
        this.stage.addChild(this.image);
        
        this.body = createBox(world,1,this.xpos*this.size+this.size/2,this.ypos*this.size+this.size/2,this.size*2,this.size*2,8,1,0.005);
        this.body.GetFixtureList().SetUserData("heavy4");
        this.body.GetFixtureList().SetSensor(false);
            
        let vertices=[];
        vertices[0]=new b2Vec2(this.size*(this.triggerlength-0.2)/scale,-this.size/scale);
        vertices[1]=new b2Vec2(this.size*(this.triggerlength-0.2)/scale,this.size/scale);
        vertices[2]=new b2Vec2(this.size*this.triggerlength/scale,this.size/scale);
        vertices[3]=new b2Vec2(this.size*this.triggerlength/scale,-this.size/scale);
    
        let fixtureDefmini = new b2FixtureDef();
        fixtureDefmini.density=0.1;
        fixtureDefmini.friction=0;
        fixtureDefmini.restitution=0;
        let shapemini = new b2PolygonShape();
        shapemini.SetAsArray(vertices,4);
        fixtureDefmini.shape=shapemini;
        fixtureDefmini.isSensor=true;
    
        let miniFixture=this.body.CreateFixture(fixtureDefmini);
        miniFixture.SetUserData("TypechangeCheckSensor4");
        }else if(this.type==9){
            
        this.image=new createjs.Bitmap("sprites/tiles/1/ground.png");
        this.image.x=this.xpos*this.size;
        this.image.y=this.ypos*this.size;
        this.image.regX=this.size/2;
        this.image.regY=this.size/2;
        this.stage.addChild(this.image);
            
    let effectData = {
    images:["sprites/effects/red_0.png","sprites/effects/red_1.png","sprites/effects/red_2.png"],
    frames:[
        [0, 0, 32, 32, 0, 16, 16],
        [0, 0, 32, 32, 1, 16, 16],
        [0, 0, 32, 32, 2, 16, 16],
    ],
    animations:{
    sparkle:[0,1,"sparkle",0.1]
        

    }
}
        let effectSheet = new createjs.SpriteSheet(effectData);
        this.image2 = new createjs.Sprite(effectSheet,"sparkle");
        this.image2.x=this.xpos*this.size;
        this.image2.y=this.ypos*this.size;
        this.stage.addChild(this.image2);
            
        this.body = createBox(world,1,this.xpos*this.size,this.ypos*this.size,this.size,this.size,1,1,0);
        this.body.GetFixtureList().SetUserData("ground");
            
        }else if(this.type==10){
            
        this.image=new createjs.Bitmap("sprites/tiles/1/ground.png");
        this.image.x=this.xpos*this.size;
        this.image.y=this.ypos*this.size;
        this.image.regX=this.size/2;
        this.image.regY=this.size/2;
        this.stage.addChild(this.image);
            
    let effectData = {
    images:["sprites/effects/blue_0.png","sprites/effects/blue_1.png","sprites/effects/blue_2.png"],
    frames:[
        [0, 0, 32, 32, 0, 16, 16],
        [0, 0, 32, 32, 1, 16, 16],
        [0, 0, 32, 32, 2, 16, 16],
    ],
    animations:{
    sparkle:[0,1,"sparkle",0.1]
        

    }
}
        let effectSheet = new createjs.SpriteSheet(effectData);
        this.image2 = new createjs.Sprite(effectSheet,"sparkle");
        this.image2.x=this.xpos*this.size;
        this.image2.y=this.ypos*this.size;
        this.stage.addChild(this.image2);
            
        this.body = createBox(world,1,this.xpos*this.size,this.ypos*this.size,this.size,this.size,1,1,0);
        this.body.GetFixtureList().SetUserData("ground");
            
        }else if(this.type==11){    
            let coinData = {
            images:["sprites/objects/coins.png"],
            frames:{width:16, height:16, count:24, regX: 8, regY:8, spacing:0, margin:0},
    animations:{
    coinanime:[0,7,"coinanime",0.2]
    }
}
        let coinSheet = new createjs.SpriteSheet(coinData);
        this.image = new createjs.Sprite(coinSheet,"coinanime");
        this.image.x=this.xpos*this.size;
        this.image.y=this.ypos*this.size;
        this.stage.addChild(this.image);  
            
        this.body = createBox(world,1,this.xpos*this.size,this.ypos*this.size,this.size/2,this.size/2,1,0,0);
        this.body.GetFixtureList().SetUserData("coin");
        this.body.GetFixtureList().SetSensor(true);
        }
         
    return this.body
     }
    
    objectsSyn(){                                                       //Synchronize the position of objects' physics body and image 
        if(this.type!=5&&this.type!=6&&this.type!=7&&this.type!=8){             //for normal objects
        let bodypos=new b2Vec2(this.image.x/scale,this.image.y/scale);
        let bodyangle=this.image.rotation*Math.PI/180;
        if(this.body){
        this.body.SetPositionAndAngle(bodypos,bodyangle);
        }
        if(this.image2){
            this.image2.x=this.image.x;
            this.image2.y=this.image.y;
        }
        }else if((this.type==5||this.type==6||this.type==7||this.type==8)&&canRotate==1){   //for falling heavy objects when are not rotating
            let bodyposition=this.body.GetPosition();
            let bodyangle=this.body.GetAngle();
            this.image.x=bodyposition.x*scale;
            this.image.y=bodyposition.y*scale;
            this.image.rotation=bodyangle/Math.PI*180;
        }else if((this.type==5||this.type==6||this.type==7||this.type==8)&&canRotate==0){      //for falling heavy objects when are rotating
            let bodypos=new b2Vec2(this.image.x/scale,this.image.y/scale);
            let bodyangle=this.image.rotation*Math.PI/180;
            this.body.SetPositionAndAngle(bodypos,bodyangle);
        }
     }
    
    objectesInteract(){                                                                         //for showing tutorial text when interact with tutorial signs
        if(this.type==1&&interact==true&&this.body.m_contactList!=null&&added==false){
            tutorialText.text='Press "A"&"D" to move.\n Try to find a way to get out here.';
            tutorialText.x=this.image.x-64;
            tutorialText.y=this.image.y-64;
            this.stage.addChild(tutorialText);
            added=true;
        }else if(this.type==2&&interact==true&&this.body.m_contactList!=null&&added==false){
            tutorialText.text='Press "W" to jump.';
            tutorialText.x=this.image.x-64;
            tutorialText.y=this.image.y-64;
            this.stage.addChild(tutorialText);
            added=true;
        }else if(this.type==3&&interact==true&&this.body.m_contactList!=null&&added==false){
            tutorialText.text="Don't touch spike!\n...and watch up!";
            tutorialText.x=this.image.x-64;
            tutorialText.y=this.image.y-64;
            this.stage.addChild(tutorialText);
            added=true;
    }else if(this.type==4&&interact==true&&this.body.m_contactList!=null&&added==false){
            tutorialText.text='If you can not jump up,\n try to press "J"&"K".';
            tutorialText.x=this.image.x-64;
            tutorialText.y=this.image.y-64;
            this.stage.addChild(tutorialText);
            added=true;
    }
}
        stopInteract(){                                                                     //for removing tutorial text after interact with tutorial signs
            this.stage.removeChild(tutorialText);
            added=false;
        }
}


function interactCheck(){                                                                   //for checking if is interacting with tutorial signs
    if(interact==true){
        for(let i=0;i<objectsarray.length;i++){
            objectsarray[i].objectesInteract();
        }
    }
        if(interact==false){
        for(let i=0;i<objectsarray.length;i++){
            objectsarray[i].stopInteract();
        }
    }
}