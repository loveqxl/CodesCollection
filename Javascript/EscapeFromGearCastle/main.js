"use strict"
let bgCanvas, canvas, objectCanvas, playerCanvas, debugCanvas, viewCanvas, UICanvas;
let bgStage, gameStage, objectStage, playerStage, world, gameView, gameUI; //bgStage for background, gameStage for map, objectStage for object, playerStage for player, world for physics, gameView for boader, gameUI for UI
let playerCharacter, playerbody,playerbodyPos;
let mapLevel=currentLevel;
let decorationGear,decorationCharaSprite;

bgCanvas=document.getElementById("bgStage");                                    //set stage layers I need
canvas=document.getElementById("gameStage");
objectCanvas=document.getElementById("objectStage");
playerCanvas=document.getElementById("playerStage");
debugCanvas=document.getElementById("debug");
let debugContext=debugCanvas.getContext('2d');
viewCanvas=document.getElementById("gameView");
UICanvas=document.getElementById("gameUI");
world = createWorld();
let debugDraw=setDebugDraw(world);
bgStage = new createjs.Stage(bgCanvas);
gameStage = new createjs.Stage(canvas);
objectStage = new createjs.Stage(objectCanvas);
playerStage = new createjs.Stage(playerCanvas);
gameView= new createjs.Stage(viewCanvas);
gameUI=new createjs.Stage(UICanvas);


let boaderDef = new createjs.Graphics().beginStroke("black").drawRect(400,300,800,600);    //screen boader
let boader = new createjs.Shape(boaderDef);
boader.regX = 400;
boader.regY = 300;
gameView.addChild(boader);

let blackscreenDef = new createjs.Graphics().beginFill("black").drawRect(0,0,800,600);     //black screen
let blackscreen = new createjs.Shape(blackscreenDef);
blackscreen.alpha=0;
boader.regX = 400;
boader.regY = 300;
gameView.addChild(blackscreen);


let bg1 = new createjs.Bitmap("sprites/BG1.png");                                   //background pictures
let bg1_2 = new createjs.Bitmap("sprites/BG1.png");
let bg1_3 = new createjs.Bitmap("sprites/BG1.png");
let bg2 = new createjs.Bitmap("sprites/BG2.png");
let bg2Alpha= new createjs.Bitmap("sprites/BG2_alpha.png");
bg2Alpha.cache(0,0,400,300);


bg2.filters=[                                                                       // alpha map for moon
    new createjs.AlphaMapFilter(bg2Alpha.cacheCanvas)
];
bg2.cache(0,0,400,300);

bg1.scaleX=2;
bg1.scaleY=2;
bg1_2.scaleX=2;
bg1_2.scaleY=2;
bg1_2.x=512;
bg1_3.scaleX=2;
bg1_3.scaleY=2;
bg1_3.x=-512;

bg2.scaleX=2;
bg2.scaleY=2;
bg2.x=300;

bgStage.addChild(bg1);
bgStage.addChild(bg1_2);
bgStage.addChild(bg1_3);
bgStage.addChild(bg2);

    
//set frame update every tick
createjs.Ticker.timingMode = createjs.Ticker.RAF_SYNCHED;                       //set game tick 
createjs.Ticker.framerate=90;
createjs.Ticker.addEventListener("tick", Tick);

let listener = new collisionCheckListener();                                    //set collision listener
world.SetContactListener(listener);

loadObjects();                                                               //load objects for all three levels 

function setup(){                                                           // setup level before start each level

    
    if(currentLevel!=0&&currentLevel!=endLevel){                            //for level 1~3

tilesarray=[];
objectsarray=[];
coinGet=false;
debugContext.setTransform({a:1,b:0,c:0,d:1,e:0,f:0});
bgStage.setTransform(0,0);
gameStage.setTransform(0,0);
objectStage.setTransform(0,0);
playerStage.setTransform(0,0);
drawMap(gameStage,currentLevel);
drawObjects(objectStage,currentLevel);
playerCharacter = new character(playerStage,world,startpoint[currentLevel][0],startpoint[currentLevel][1],18,26,2,0.2,0,false,22,18);
playerCharacter.createCharacter();
playerbody=playerCharacter.characterBody;
playerbodyPos=playerbody.GetBodyA().GetPosition();
alive=true;
characterStatus=0;


    }else if(currentLevel==0){                                                //for start screen
    currentCoin=0;
    coinGet=false;
        
let decorationChara = new character(gameUI,world,190,90,18,26,2,0.2,0,false,20,18);
    decorationCharaSprite = decorationChara.createCharacterSprite();
    decorationCharaSprite.gotoAndPlay("run");
 
    decorationGear = new createjs.Bitmap("sprites/gear.png");
    decorationGear.regX=180;
    decorationGear.regY=180;    
    decorationGear.x=590;
    decorationGear.y=200;
    decorationGear.scale=0.2;
    gameUI.addChild(decorationGear);
        
let decorationTileAnime = createjs.Tween.get(decorationGear).to({rotation:360},3000);
    decorationTileAnime.loop=-1;
                
let titletext = new createjs.Bitmap("sprites/title.PNG");
titletext.alpha=1;
titletext.y=100;
gameUI.addChild(titletext);
        
let starttext = new createjs.Bitmap("sprites/start.PNG");
starttext.alpha=1;
starttext.y=400;
gameUI.addChild(starttext);
        
let starttween=createjs.Tween.get(starttext).to({alpha:0},1000).to({alpha:1},1000);
starttween.loop=-1;
        
document.addEventListener("keydown",starttogame); 
        
        
    }else if(currentLevel==endLevel){                                       //for end and credit screen
let leveltitle = new createjs.Text("", "40px Arial", "#FFFFFF");
        let coinShow1,coinShow2,coinShow3;
        
        let coinData = {
            images:["sprites/objects/coins.png"],
            frames:{width:16, height:16, count:24, regX: 8, regY:8, spacing:0, margin:0},
    animations:{
    coinanime:[0,7,"coinanime",0.2],
    nocoinanime:[16,23,"nocoinanime",0.2]
    }
}
        let coinSheet = new createjs.SpriteSheet(coinData);
    if(currentCoin==3){
                coinShow1=new createjs.Sprite(coinSheet,"coinanime");
                coinShow2=new createjs.Sprite(coinSheet,"coinanime");
                coinShow3=new createjs.Sprite(coinSheet,"coinanime");
            }else if(currentCoin==2){
                coinShow1=new createjs.Sprite(coinSheet,"coinanime");
                coinShow2=new createjs.Sprite(coinSheet,"coinanime");
                coinShow3=new createjs.Sprite(coinSheet,"nocoinanime");
            }else if(currentCoin==1){
                coinShow1=new createjs.Sprite(coinSheet,"coinanime");
                coinShow2=new createjs.Sprite(coinSheet,"nocoinanime");
                coinShow3=new createjs.Sprite(coinSheet,"nocoinanime");
            }else if(currentCoin==0){
                coinShow1=new createjs.Sprite(coinSheet,"nocoinanime");
                coinShow2=new createjs.Sprite(coinSheet,"nocoinanime");
                coinShow3=new createjs.Sprite(coinSheet,"nocoinanime");
            }
         coinShow1.x=350;
         coinShow1.y=350;
         coinShow1.scale=2;
         coinShow1.alpha=0;
         gameUI.addChild(coinShow1); 
         coinShow2.x=400;
         coinShow2.y=350;
         coinShow2.scale=2;
         coinShow2.alpha=0;
         gameUI.addChild(coinShow2); 
         coinShow3.x=450;
         coinShow3.y=350;
         coinShow3.scale=2;
         coinShow3.alpha=0;
         gameUI.addChild(coinShow3); 
  


        
let congratext = new createjs.Bitmap("sprites/congra.PNG"); 
    congratext.alpha=0;
    congratext.y=200;
    gameUI.addChild(congratext);
    createjs.Tween.get(coinShow1).to({alpha:1},500).wait(2000).to({alpha:0},500);
    createjs.Tween.get(coinShow2).to({alpha:1},500).wait(2000).to({alpha:0},500);
    createjs.Tween.get(coinShow3).to({alpha:1},500).wait(2000).to({alpha:0},500);
    createjs.Tween.get(congratext).to({alpha:1},1000).wait(1000).to({alpha:0},1000).call(tocredit);
    
    coinGet=false;
        
let credittext = new createjs.Bitmap("sprites/credit.png");
    credittext.y=600;
    gameUI.addChild(credittext);
    
    function tocredit(){
    createjs.Tween.get(credittext).to({y:-800},11000).call(totheend);
    }
        
let endtext = new createjs.Bitmap("sprites/end.PNG");
    endtext.alpha=0;    
    endtext.y=150;
    gameUI.addChild(endtext);

    let restarttext = new createjs.Bitmap("sprites/restart.PNG");
    restarttext.alpha=0;
    restarttext.y=350;
    gameUI.addChild(restarttext); 
        
    function totheend(){
    let restarttween=createjs.Tween.get(restarttext).to({alpha:1},1000).to({alpha:0},1000);
    restarttween.loop=-1;
    createjs.Tween.get(endtext).to({alpha:1},500).call(addlisten);
    }
    function addlisten(){
        document.addEventListener("keydown",tothestart);
    }
    function tothestart(){
    document.removeEventListener("keydown",tothestart);
    createjs.Tween.get(restarttext).to({alpha:0},500);
    createjs.Tween.get(endtext).to({alpha:0},500).call(reset);
    
    } 
    function reset(){
        gameUI.removeAllChildren();     
        changeLevel();
    }    
    }
}

setup();                                                //setup game at first

function Tick(e){                                       //for what the game do each tick/frame
    world.Step(1.0 / 60, 1);
    world.ClearForces();                                // physic world simulate
    
    if(currentLevel!=0&&currentLevel!=endLevel&&playerCharacter){           //when is in level 1~3
    //debugContext.setTransform({a:1,b:0,c:0,d:1,e:-playerbodyPos.x*scale+playerCanvas.width/2,f:-playerbodyPos.y*scale+playerCanvas.height/2});
    //debugContext.clearRect(+playerbodyPos.x*scale-playerCanvas.width/2,+playerbodyPos.y*scale-playerCanvas.height/2,800,600);
    //world.DrawDebugData();
    
    bgStage.setTransform((-playerbodyPos.x+playerCanvas.width/2/scale)*2,0);                                            // camera follow the character
    gameStage.setTransform(-playerbodyPos.x*scale+playerCanvas.width/2,-playerbodyPos.y*scale+playerCanvas.height/2);
    objectStage.setTransform(-playerbodyPos.x*scale+playerCanvas.width/2,-playerbodyPos.y*scale+playerCanvas.height/2);
    playerStage.setTransform(-playerbodyPos.x*scale+playerCanvas.width/2,-playerbodyPos.y*scale+playerCanvas.height/2);
    playerCharacter.characterMove();                                                                                      //character move
    deadCheck();                                                                               //character dead check
    interactCheck();                                                                    //tutorial interactive check


    playerCharacter.characterSynchronize();                                         //Synchronize position of character's body and sprite 
    if(tilesarray!=[]){                                                              //Synchronize position of each tiles' body and image
    for(let i=0;i<tilesarray.length;i++){
    tilesarray[i].tilesSyn();
    }
    }
    
    if(objectsarray!=[]){                                                       //Synchronize position of each objects' body and image
    for(let i=0;i<objectsarray.length;i++){
    objectsarray[i].objectsSyn();
    }
    }
        
    }

    checkLevelChange();                                                             //check if should change level
    
    bgStage.update(e);                                                              //update graphics for every stage layer
    gameStage.update(e);
    objectStage.update(e);
    playerStage.update(e); //draw gameStage every tick
    gameView.update(e);
    gameUI.update(e);
    
}










