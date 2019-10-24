class tiles{                                                        //class of tiles
    constructor(stage,type,xpos,ypos){
        this.stage=stage;
        this.type=type;
        this.xpos=xpos;
        this.ypos=ypos;
        this.size=32;
        this.image;
        this.image2;
        this.body;
    }
    
    createTile(stage,xpos,ypos,rotation=0){                         //creating tiles according to its type, 1 for wall, 2 for ground, 3~6 for 45 degree slope, 7~14 for 30 degree slope, 15 for target end door, 16 for start door, 17~20 for spike
        if(this.type==1){
        this.image2=new createjs.Bitmap("sprites/tiles/2/wall.png");
        this.image2.x=this.xpos*this.size;
        this.image2.y=this.ypos*this.size;
        this.image2.regX=this.size/2;
        this.image2.regY=this.size/2;
        this.stage.addChild(this.image2);

        }else if(this.type==2){
        this.image2=new createjs.Bitmap("sprites/tiles/1/ground.png");
        this.image2.x=this.xpos*this.size;
        this.image2.y=this.ypos*this.size;
        this.image2.regX=this.size/2;
        this.image2.regY=this.size/2;
        this.stage.addChild(this.image2);
        
        this.body = createBox(world,1,this.xpos*this.size,this.ypos*this.size,this.size-1,this.size-1,1,1,0);
        this.body.GetFixtureList().SetUserData("ground");

        }else if(this.type==3){
            
        this.image=new createjs.Bitmap("sprites/tiles/2/wall.png");
        this.image.x=this.xpos*this.size;
        this.image.y=this.ypos*this.size;
        this.image.regX=this.size/2;
        this.image.regY=this.size/2;
        this.stage.addChild(this.image);
            
        this.image2=new createjs.Bitmap("sprites/tiles/1/slope1.png");
        this.image2.x=this.xpos*this.size;
        this.image2.y=this.ypos*this.size;
        this.image2.regX=this.size/2;
        this.image2.regY=this.size/2;
        this.stage.addChild(this.image2);
        
        
        let bodyDef = new b2BodyDef();
        bodyDef.position = new b2Vec2(this.xpos*this.size/scale,this.ypos*this.size/scale);
        bodyDef.type=1;
        bodyDef.allowSleep=true;
    
        let fixtureDef = new b2FixtureDef();
        fixtureDef.density=1;
        fixtureDef.friction=6;
        fixtureDef.restitution=0;
        let shape= new b2PolygonShape();
        let vecArray=[];
            vecArray[0]=new b2Vec2(-15/scale,15/scale);
            vecArray[1]=new b2Vec2(15/scale,-15/scale);
            vecArray[2]=new b2Vec2(15/scale,15/scale);
        shape.SetAsArray(vecArray,3);    
        fixtureDef.shape=shape;
        this.body=world.CreateBody(bodyDef);
        this.body.CreateFixture(fixtureDef);
        this.body.GetFixtureList().SetUserData("ground");
        }else if(this.type==4){
        this.image=new createjs.Bitmap("sprites/tiles/2/wall.png");
        this.image.x=this.xpos*this.size;
        this.image.y=this.ypos*this.size;
        this.image.regX=this.size/2;
        this.image.regY=this.size/2;
        this.stage.addChild(this.image);
        
        this.image2=new createjs.Bitmap("sprites/tiles/1/slope1_90.png");
        this.image2.x=this.xpos*this.size;
        this.image2.y=this.ypos*this.size;
        this.image2.regX=this.size/2;
        this.image2.regY=this.size/2;
        this.stage.addChild(this.image2);
        
        
        let bodyDef = new b2BodyDef();
        bodyDef.position = new b2Vec2(this.xpos*this.size/scale,this.ypos*this.size/scale);
        bodyDef.type=1;
        bodyDef.allowSleep=true;
    
        let fixtureDef = new b2FixtureDef();
        fixtureDef.density=1;
        fixtureDef.friction=6;
        fixtureDef.restitution=0;
        let shape= new b2PolygonShape();
        let vecArray=[];
            vecArray[0]=new b2Vec2(-15/scale,15/scale);
            vecArray[1]=new b2Vec2(-15/scale,-15/scale);
            vecArray[2]=new b2Vec2(15/scale,15/scale);
        shape.SetAsArray(vecArray,3);    
        fixtureDef.shape=shape;
        this.body=world.CreateBody(bodyDef);
        this.body.CreateFixture(fixtureDef);
        }else if(this.type==5){
        this.image=new createjs.Bitmap("sprites/tiles/2/wall.png");
        this.image.x=this.xpos*this.size;
        this.image.y=this.ypos*this.size;
        this.image.regX=this.size/2;
        this.image.regY=this.size/2;
        this.stage.addChild(this.image);
        
        this.image2=new createjs.Bitmap("sprites/tiles/1/slope1_180.png");
        this.image2.x=this.xpos*this.size;
        this.image2.y=this.ypos*this.size;
        this.image2.regX=this.size/2;
        this.image2.regY=this.size/2;
        this.stage.addChild(this.image2);
        
        
        let bodyDef = new b2BodyDef();
        bodyDef.position = new b2Vec2(this.xpos*this.size/scale,this.ypos*this.size/scale);
        bodyDef.type=1;
        bodyDef.allowSleep=true;
    
        let fixtureDef = new b2FixtureDef();
        fixtureDef.density=1;
        fixtureDef.friction=6;
        fixtureDef.restitution=0;
        let shape= new b2PolygonShape();
        let vecArray=[];
            vecArray[0]=new b2Vec2(-16/scale,16/scale);
            vecArray[1]=new b2Vec2(-16/scale,-16/scale);
            vecArray[2]=new b2Vec2(16/scale,-16/scale);
        shape.SetAsArray(vecArray,3);    
        fixtureDef.shape=shape;
        this.body=world.CreateBody(bodyDef);
        this.body.CreateFixture(fixtureDef); 
        this.body.GetFixtureList().SetUserData("ground");
        }else if(this.type==6){
        this.image=new createjs.Bitmap("sprites/tiles/2/wall.png");
        this.image.x=this.xpos*this.size;
        this.image.y=this.ypos*this.size;
        this.image.regX=this.size/2;
        this.image.regY=this.size/2;
        this.stage.addChild(this.image);    
            
        this.image2=new createjs.Bitmap("sprites/tiles/1/slope1_270.png");
        this.image2.x=this.xpos*this.size;
        this.image2.y=this.ypos*this.size;
        this.image2.regX=this.size/2;
        this.image2.regY=this.size/2;
        this.stage.addChild(this.image2);
        
        
        let bodyDef = new b2BodyDef();
        bodyDef.position = new b2Vec2(this.xpos*this.size/scale,this.ypos*this.size/scale);
        bodyDef.type=1;
        bodyDef.allowSleep=true;
    
        let fixtureDef = new b2FixtureDef();
        fixtureDef.density=1;
        fixtureDef.friction=6;
        fixtureDef.restitution=0;
        let shape= new b2PolygonShape();
        let vecArray=[];
            vecArray[0]=new b2Vec2(-15/scale,-15/scale);
            vecArray[1]=new b2Vec2(15/scale,-15/scale);
            vecArray[2]=new b2Vec2(15/scale,15/scale);
        shape.SetAsArray(vecArray,3);    
        fixtureDef.shape=shape;
        this.body=world.CreateBody(bodyDef);
        this.body.CreateFixture(fixtureDef);    
        }else if(this.type==7){
        this.image=new createjs.Bitmap("sprites/tiles/2/wall.png");
        this.image.x=this.xpos*this.size;
        this.image.y=this.ypos*this.size;
        this.image.regX=this.size/2;
        this.image.regY=this.size/2;
        this.stage.addChild(this.image);
            
        this.image2=new createjs.Bitmap("sprites/tiles/1/slope2_1.png");
        this.image2.x=this.xpos*this.size;
        this.image2.y=this.ypos*this.size;
        this.image2.regX=this.size/2;
        this.image2.regY=this.size/2;
        this.image2.rotation=0;
        this.stage.addChild(this.image2);
        
        
        let bodyDef = new b2BodyDef();
        bodyDef.position = new b2Vec2(this.xpos*this.size/scale,this.ypos*this.size/scale);
        bodyDef.type=1;
        bodyDef.allowSleep=true;
    
        let fixtureDef = new b2FixtureDef();
        fixtureDef.density=1;
        fixtureDef.friction=2;
        fixtureDef.restitution=0;
        let shape= new b2PolygonShape();
        let vecArray=[];
            vecArray[0]=new b2Vec2(-15/scale,15/scale);
            vecArray[1]=new b2Vec2(15/scale,0/scale);
            vecArray[2]=new b2Vec2(15/scale,15/scale);
        shape.SetAsArray(vecArray,3);    
        fixtureDef.shape=shape;
        this.body=world.CreateBody(bodyDef);
        this.body.CreateFixture(fixtureDef);
        this.body.GetFixtureList().SetUserData("ground");
        }else if(this.type==8){
        this.image=new createjs.Bitmap("sprites/tiles/2/wall.png");
        this.image.x=this.xpos*this.size;
        this.image.y=this.ypos*this.size;
        this.image.regX=this.size/2;
        this.image.regY=this.size/2;
        this.stage.addChild(this.image);    
            
        this.image2=new createjs.Bitmap("sprites/tiles/1/slope2_2.png");
        this.image2.x=this.xpos*this.size;
        this.image2.y=this.ypos*this.size;
        this.image2.regX=this.size/2;
        this.image2.regY=this.size/2;
        this.image2.rotation=0;
        this.stage.addChild(this.image2);
        
        
        let bodyDef = new b2BodyDef();
        bodyDef.position = new b2Vec2(this.xpos*this.size/scale,this.ypos*this.size/scale);
        bodyDef.type=1;
        bodyDef.allowSleep=true;
    
        let fixtureDef = new b2FixtureDef();
        fixtureDef.density=1;
        fixtureDef.friction=2;
        fixtureDef.restitution=0;
        let shape= new b2PolygonShape();
        let vecArray=[];
            vecArray[0]=new b2Vec2(-15/scale,15/scale);
            vecArray[1]=new b2Vec2(-15/scale,0/scale);
            vecArray[2]=new b2Vec2(15/scale,-15/scale);
            vecArray[3]=new b2Vec2(15/scale,15/scale);
        shape.SetAsArray(vecArray,4);    
        fixtureDef.shape=shape;
        this.body=world.CreateBody(bodyDef);
        this.body.CreateFixture(fixtureDef);
        this.body.GetFixtureList().SetUserData("ground");
        }else if(this.type==9){
        this.image=new createjs.Bitmap("sprites/tiles/2/wall.png");
        this.image.x=this.xpos*this.size;
        this.image.y=this.ypos*this.size;
        this.image.regX=this.size/2;
        this.image.regY=this.size/2;
        this.stage.addChild(this.image);
        
        this.image2=new createjs.Bitmap("sprites/tiles/1/slope2_1_left.png");
        this.image2.x=this.xpos*this.size;
        this.image2.y=this.ypos*this.size;
        this.image2.regX=this.size/2;
        this.image2.regY=this.size/2;
        this.stage.addChild(this.image2);
        
        
        let bodyDef = new b2BodyDef();
        bodyDef.position = new b2Vec2(this.xpos*this.size/scale,this.ypos*this.size/scale);
        bodyDef.type=1;
        bodyDef.allowSleep=true;
    
        let fixtureDef = new b2FixtureDef();
        fixtureDef.density=1;
        fixtureDef.friction=2;
        fixtureDef.restitution=0;
        let shape= new b2PolygonShape();
        let vecArray=[];
            vecArray[0]=new b2Vec2(-15/scale,15/scale);
            vecArray[1]=new b2Vec2(-15/scale,0/scale);
            vecArray[2]=new b2Vec2(15/scale,15/scale);
        shape.SetAsArray(vecArray,3);    
        fixtureDef.shape=shape;
        this.body=world.CreateBody(bodyDef);
        this.body.CreateFixture(fixtureDef); 
        this.body.GetFixtureList().SetUserData("ground");
        }else if(this.type==10){
        this.image=new createjs.Bitmap("sprites/tiles/2/wall.png");
        this.image.x=this.xpos*this.size;
        this.image.y=this.ypos*this.size;
        this.image.regX=this.size/2;
        this.image.regY=this.size/2;
        this.stage.addChild(this.image);   
        
        this.image2=new createjs.Bitmap("sprites/tiles/1/slope2_2_left.png");
        this.image2.x=this.xpos*this.size;
        this.image2.y=this.ypos*this.size;
        this.image2.regX=this.size/2;
        this.image2.regY=this.size/2;
        this.stage.addChild(this.image2);
        
        
        let bodyDef = new b2BodyDef();
        bodyDef.position = new b2Vec2(this.xpos*this.size/scale,this.ypos*this.size/scale);
        bodyDef.type=1;
        bodyDef.allowSleep=true;
    
        let fixtureDef = new b2FixtureDef();
        fixtureDef.density=1;
        fixtureDef.friction=2;
        fixtureDef.restitution=0;
        let shape= new b2PolygonShape();
        let vecArray=[];
            vecArray[0]=new b2Vec2(-15/scale,15/scale);
            vecArray[1]=new b2Vec2(-15/scale,-15/scale);
            vecArray[2]=new b2Vec2(15/scale,0/scale);
            vecArray[3]=new b2Vec2(15/scale,15/scale);
        shape.SetAsArray(vecArray,4);    
        fixtureDef.shape=shape;
        this.body=world.CreateBody(bodyDef);
        this.body.CreateFixture(fixtureDef);
        this.body.GetFixtureList().SetUserData("ground");
        }else if(this.type==11){
        this.image=new createjs.Bitmap("sprites/tiles/2/wall.png");
        this.image.x=this.xpos*this.size;
        this.image.y=this.ypos*this.size;
        this.image.regX=this.size/2;
        this.image.regY=this.size/2;
        this.stage.addChild(this.image);
            
        this.image2=new createjs.Bitmap("sprites/tiles/1/slope2_1_down.png");
        this.image2.x=this.xpos*this.size;
        this.image2.y=this.ypos*this.size;
        this.image2.regX=this.size/2;
        this.image2.regY=this.size/2;
        this.stage.addChild(this.image2);
        
        
        let bodyDef = new b2BodyDef();
        bodyDef.position = new b2Vec2(this.xpos*this.size/scale,this.ypos*this.size/scale);
        bodyDef.type=1;
        bodyDef.allowSleep=true;
    
        let fixtureDef = new b2FixtureDef();
        fixtureDef.density=1;
        fixtureDef.friction=2;
        fixtureDef.restitution=0;
        let shape= new b2PolygonShape();
        let vecArray=[];
            vecArray[0]=new b2Vec2(-15/scale,-15/scale);
            vecArray[1]=new b2Vec2(15/scale,-15/scale);
            vecArray[2]=new b2Vec2(15/scale,0/scale);
        shape.SetAsArray(vecArray,3);    
        fixtureDef.shape=shape;
        this.body=world.CreateBody(bodyDef);
        this.body.CreateFixture(fixtureDef); 
        this.body.GetFixtureList().SetUserData("ground");
        }else if(this.type==12){
        this.image=new createjs.Bitmap("sprites/tiles/2/wall.png");
        this.image.x=this.xpos*this.size;
        this.image.y=this.ypos*this.size;
        this.image.regX=this.size/2;
        this.image.regY=this.size/2;
        this.stage.addChild(this.image);
            
        this.image2=new createjs.Bitmap("sprites/tiles/1/slope2_2_down.png");
        this.image2.x=this.xpos*this.size;
        this.image2.y=this.ypos*this.size;
        this.image2.regX=this.size/2;
        this.image2.regY=this.size/2;
        this.stage.addChild(this.image2);
        
        
        let bodyDef = new b2BodyDef();
        bodyDef.position = new b2Vec2(this.xpos*this.size/scale,this.ypos*this.size/scale);
        bodyDef.type=1;
        bodyDef.allowSleep=true;
    
        let fixtureDef = new b2FixtureDef();
        fixtureDef.density=1;
        fixtureDef.friction=2;
        fixtureDef.restitution=0;
        let shape= new b2PolygonShape();
        let vecArray=[];
            vecArray[0]=new b2Vec2(-15/scale,0/scale);
            vecArray[1]=new b2Vec2(-15/scale,-15/scale);
            vecArray[2]=new b2Vec2(15/scale,-15/scale);
            vecArray[3]=new b2Vec2(15/scale,15/scale);
        shape.SetAsArray(vecArray,4);    
        fixtureDef.shape=shape;
        this.body=world.CreateBody(bodyDef);
        this.body.CreateFixture(fixtureDef);
        this.body.GetFixtureList().SetUserData("ground");
        }else if(this.type==13){
        this.image=new createjs.Bitmap("sprites/tiles/2/wall.png");
        this.image.x=this.xpos*this.size;
        this.image.y=this.ypos*this.size;
        this.image.regX=this.size/2;
        this.image.regY=this.size/2;
        this.stage.addChild(this.image);
        
        this.image2=new createjs.Bitmap("sprites/tiles/1/slope2_1_downleft.png");
        this.image2.x=this.xpos*this.size;
        this.image2.y=this.ypos*this.size;
        this.image2.regX=this.size/2;
        this.image2.regY=this.size/2;
        this.stage.addChild(this.image2);
        
        
        let bodyDef = new b2BodyDef();
        bodyDef.position = new b2Vec2(this.xpos*this.size/scale,this.ypos*this.size/scale);
        bodyDef.type=1;
        bodyDef.allowSleep=true;
    
        let fixtureDef = new b2FixtureDef();
        fixtureDef.density=1;
        fixtureDef.friction=2;
        fixtureDef.restitution=0;
        let shape= new b2PolygonShape();
        let vecArray=[];
            vecArray[0]=new b2Vec2(-15/scale,0/scale);
            vecArray[1]=new b2Vec2(-15/scale,-15/scale);
            vecArray[2]=new b2Vec2(15/scale,-15/scale);
        shape.SetAsArray(vecArray,3);    
        fixtureDef.shape=shape;
        this.body=world.CreateBody(bodyDef);
        this.body.CreateFixture(fixtureDef);
        this.body.GetFixtureList().SetUserData("ground");
        }else if(this.type==14){
        this.image=new createjs.Bitmap("sprites/tiles/2/wall.png");
        this.image.x=this.xpos*this.size;
        this.image.y=this.ypos*this.size;
        this.image.regX=this.size/2;
        this.image.regY=this.size/2;
        this.stage.addChild(this.image);
            
        this.image2=new createjs.Bitmap("sprites/tiles/1/slope2_2_downleft.png");
        this.image2.x=this.xpos*this.size;
        this.image2.y=this.ypos*this.size;
        this.image2.regX=this.size/2;
        this.image2.regY=this.size/2;
        this.stage.addChild(this.image2);
        
        
        let bodyDef = new b2BodyDef();
        bodyDef.position = new b2Vec2(this.xpos*this.size/scale,this.ypos*this.size/scale);
        bodyDef.type=1;
        bodyDef.allowSleep=true;
    
        let fixtureDef = new b2FixtureDef();
        fixtureDef.density=1;
        fixtureDef.friction=2;
        fixtureDef.restitution=0;
        let shape= new b2PolygonShape();
        let vecArray=[];
            vecArray[0]=new b2Vec2(-15/scale,15/scale);
            vecArray[1]=new b2Vec2(-15/scale,-15/scale);
            vecArray[2]=new b2Vec2(15/scale,-15/scale);
            vecArray[3]=new b2Vec2(15/scale,0/scale);
        shape.SetAsArray(vecArray,4);    
        fixtureDef.shape=shape;
        this.body=world.CreateBody(bodyDef);
        this.body.CreateFixture(fixtureDef);
        this.body.GetFixtureList().SetUserData("ground");
        }else if(this.type==15){
        this.image=new createjs.Bitmap("sprites/tiles/2/open_door.png");
        this.image.x=this.xpos*this.size;
        this.image.y=this.ypos*this.size;
        this.image.regX=this.size/2;
        this.image.regY=this.size/2;
        this.stage.addChild(this.image);    
            
        this.image2=new createjs.Bitmap("sprites/tiles/2/closed_door.png");
        this.image2.x=this.xpos*this.size;
        this.image2.y=this.ypos*this.size;
        this.image2.regX=this.size/2;
        this.image2.regY=this.size/2;
        this.stage.addChild(this.image2);
        
        this.body = createBox(world,1,this.xpos*this.size,this.ypos*this.size,this.size,this.size,1,1,0);
        this.body.GetFixtureList().SetUserData("closeddoor");
        this.body.GetFixtureList().SetSensor(true);

        }else if(this.type==16){
        this.image=new createjs.Bitmap("sprites/tiles/2/wall.png");
        this.image.x=this.xpos*this.size;
        this.image.y=this.ypos*this.size;
        this.image.regX=this.size/2;
        this.image.regY=this.size/2;
        this.stage.addChild(this.image);
            
        this.image2=new createjs.Bitmap("sprites/tiles/2/startdoor.png");
        this.image2.x=this.xpos*this.size;
        this.image2.y=this.ypos*this.size;
        this.image2.regX=this.size/2;
        this.image2.regY=this.size/2;
        this.stage.addChild(this.image2);
        
        this.body = createBox(world,1,this.xpos*this.size,this.ypos*this.size,this.size,this.size,1,1,0);
        this.body.GetFixtureList().SetUserData("startdoor");
        this.body.GetFixtureList().SetSensor(true);

        }else if(this.type==17){
                    this.image=new createjs.Bitmap("sprites/tiles/2/wall.png");
        this.image.x=this.xpos*this.size;
        this.image.y=this.ypos*this.size;
        this.image.regX=this.size/2;
        this.image.regY=this.size/2;
        this.stage.addChild(this.image);
            
        this.image2=new createjs.Bitmap("sprites/tiles/1/Spike.png");
        this.image2.x=this.xpos*this.size;
        this.image2.y=this.ypos*this.size;
        this.image2.regX=this.size/2;
        this.image2.regY=this.size/2;
        this.stage.addChild(this.image2);
        
        
        let bodyDef = new b2BodyDef();
        bodyDef.position = new b2Vec2(this.xpos*this.size/scale,this.ypos*this.size/scale);
        bodyDef.type=1;
        bodyDef.allowSleep=true;
    
        let fixtureDef = new b2FixtureDef();
        fixtureDef.density=1;
        fixtureDef.friction=2;
        fixtureDef.restitution=0;
        let shape= new b2PolygonShape();
        let vecArray=[];
            vecArray[0]=new b2Vec2(-15/scale,15/scale);
            vecArray[1]=new b2Vec2(0/scale,-15/scale);
            vecArray[2]=new b2Vec2(15/scale,15/scale);
        shape.SetAsArray(vecArray,3);    
        fixtureDef.shape=shape;
        this.body=world.CreateBody(bodyDef);
        this.body.CreateFixture(fixtureDef);
        this.body.GetFixtureList().SetUserData("spike");
        }else if(this.type==18){
                    this.image=new createjs.Bitmap("sprites/tiles/2/wall.png");
        this.image.x=this.xpos*this.size;
        this.image.y=this.ypos*this.size;
        this.image.regX=this.size/2;
        this.image.regY=this.size/2;
        this.stage.addChild(this.image);
            
        this.image2=new createjs.Bitmap("sprites/tiles/1/Spike_90.png");
        this.image2.x=this.xpos*this.size;
        this.image2.y=this.ypos*this.size;
        this.image2.regX=this.size/2;
        this.image2.regY=this.size/2;
        this.stage.addChild(this.image2);
        
        
        let bodyDef = new b2BodyDef();
        bodyDef.position = new b2Vec2(this.xpos*this.size/scale,this.ypos*this.size/scale);
        bodyDef.type=1;
        bodyDef.allowSleep=true;
    
        let fixtureDef = new b2FixtureDef();
        fixtureDef.density=1;
        fixtureDef.friction=2;
        fixtureDef.restitution=0;
        let shape= new b2PolygonShape();
        let vecArray=[];
            vecArray[0]=new b2Vec2(-15/scale,15/scale);
            vecArray[1]=new b2Vec2(-15/scale,-15/scale);
            vecArray[2]=new b2Vec2(15/scale,0/scale);
        shape.SetAsArray(vecArray,3);    
        fixtureDef.shape=shape;
        this.body=world.CreateBody(bodyDef);
        this.body.CreateFixture(fixtureDef);
        this.body.GetFixtureList().SetUserData("spike");
        }else if(this.type==19){
                    this.image=new createjs.Bitmap("sprites/tiles/2/wall.png");
        this.image.x=this.xpos*this.size;
        this.image.y=this.ypos*this.size;
        this.image.regX=this.size/2;
        this.image.regY=this.size/2;
        this.stage.addChild(this.image);
            
        this.image2=new createjs.Bitmap("sprites/tiles/1/Spike_180.png");
        this.image2.x=this.xpos*this.size;
        this.image2.y=this.ypos*this.size;
        this.image2.regX=this.size/2;
        this.image2.regY=this.size/2;
        this.stage.addChild(this.image2);
        
        
        let bodyDef = new b2BodyDef();
        bodyDef.position = new b2Vec2(this.xpos*this.size/scale,this.ypos*this.size/scale);
        bodyDef.type=1;
        bodyDef.allowSleep=true;
    
        let fixtureDef = new b2FixtureDef();
        fixtureDef.density=1;
        fixtureDef.friction=2;
        fixtureDef.restitution=0;
        let shape= new b2PolygonShape();
        let vecArray=[];
            vecArray[0]=new b2Vec2(-15/scale,-15/scale);
            vecArray[1]=new b2Vec2(15/scale,-15/scale);
            vecArray[2]=new b2Vec2(0/scale,15/scale);
        shape.SetAsArray(vecArray,3);    
        fixtureDef.shape=shape;
        this.body=world.CreateBody(bodyDef);
        this.body.CreateFixture(fixtureDef);
        this.body.GetFixtureList().SetUserData("spike");
        }else if(this.type==20){
                    this.image=new createjs.Bitmap("sprites/tiles/2/wall.png");
        this.image.x=this.xpos*this.size;
        this.image.y=this.ypos*this.size;
        this.image.regX=this.size/2;
        this.image.regY=this.size/2;
        this.stage.addChild(this.image);
            
        this.image2=new createjs.Bitmap("sprites/tiles/1/Spike_270.png");
        this.image2.x=this.xpos*this.size;
        this.image2.y=this.ypos*this.size;
        this.image2.regX=this.size/2;
        this.image2.regY=this.size/2;
        this.stage.addChild(this.image2);
        
        
        let bodyDef = new b2BodyDef();
        bodyDef.position = new b2Vec2(this.xpos*this.size/scale,this.ypos*this.size/scale);
        bodyDef.type=1;
        bodyDef.allowSleep=true;
    
        let fixtureDef = new b2FixtureDef();
        fixtureDef.density=1;
        fixtureDef.friction=2;
        fixtureDef.restitution=0;
        let shape= new b2PolygonShape();
        let vecArray=[];
            vecArray[0]=new b2Vec2(-15/scale,0/scale);
            vecArray[1]=new b2Vec2(15/scale,-15/scale);
            vecArray[2]=new b2Vec2(15/scale,15/scale);
        shape.SetAsArray(vecArray,3);    
        fixtureDef.shape=shape;
        this.body=world.CreateBody(bodyDef);
        this.body.CreateFixture(fixtureDef);
        this.body.GetFixtureList().SetUserData("spike");
        }
    return this.body
    }
    
    tilesSyn(){                                         //Synchronize the position of each tiles' physics body and image
        let bodypos=new b2Vec2(this.image2.x/scale,this.image2.y/scale);
        let bodyangle=this.image2.rotation*Math.PI/180;
        if(this.image){
        this.image.x=this.image2.x;
        this.image.y=this.image2.y;
        this.image.rotation=this.image2.rotation;
        }
        if(this.body){
        this.body.SetPositionAndAngle(bodypos,bodyangle);
        }
     }

    

}
