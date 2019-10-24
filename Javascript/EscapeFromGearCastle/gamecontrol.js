"use strict"
let currentLevel=0;
let endLevel=4;
let alive=true;
let stopStep=false;
let isBlack=false;
let coinGet=false;
let currentCoin=0;
let jointcount,bodycount,contactcount,nowajoint,nowabody,nowacontact;

function deadCheck(){                   //for dead check at each frame
if(alive==false&&isBlack==false){
    playerCharacter.dead();
    isBlack=true;
    if(coinGet==true){
    currentCoin=currentCoin-1;
    coinGet=false;
}
    black();
    document.removeEventListener("keydown",playerCharacter.statusChange);
    document.removeEventListener("keyup",playerCharacter.statusBack);
}
}

function restart(){                     //restart game
    clearWorld();
    gameStage.removeAllChildren();
    playerStage.removeAllChildren();
    objectStage.removeAllChildren();
    gameUI.removeAllChildren();
    setTimeout(setup,500);
    setTimeout(white,1000);
}


function clearWorld(){                          //remove all bodies, joints and contacts in physics world
    jointcount=world.GetJointCount();
    for(let i=0;i<jointcount;i++){
    nowajoint=world.GetJointList();
    world.DestroyJoint(nowajoint);
    }
    bodycount=world.GetBodyCount();
    for(let i=0;i<bodycount;i++){
    nowabody=world.GetBodyList();
    world.DestroyBody(nowabody);
    }
    contactcount=world.GetBodyCount();
    for(let i=0;i<contactcount;i++){
    nowacontact=world.GetBodyList();
    world.DestroyBody(nowacontact);
    }
   
}

function starttogame(e){                            //specially for start screen to level 1
    if(event.keyCode==13){      
        document.removeEventListener("keydown",starttogame);
        createjs.Tween.get(decorationCharaSprite).to({alpha:0},1000);
        createjs.Tween.get(decorationGear).to({alpha:0},1000);
        setTimeout(changeLevel,500);
    }
}


function changeLevel(){                             // change to next level
    if(currentLevel!=endLevel){
    currentLevel=currentLevel+1;
    }else{
    currentLevel=0;
    }
}

function checkLevelChange(){                        //for checking if player complete current level, if completed, show black screen and prepare next level.
if(mapLevel!=currentLevel){
    isBlack=true;
    black();
    debugContext.clearRect(-4000,-4000,8000,8000);
    if(currentLevel!=endLevel){                     //when next level is not the end 
    if(currentLevel!=0){                            //when next level is not the first level because there's no need to show coin animation at the start of the first level
    showLevel();}
    function showLevel(){                                   //for create level text and coin animation when screen is black
        let leveltitle = new createjs.Text("", "40px Arial", "#FFFFFF");
        let coinShow;
        
        let coinData = {
            images:["sprites/objects/coins.png"],
            frames:{width:16, height:16, count:24, regX: 8, regY:8, spacing:0, margin:0},
    animations:{
    coinanime:[0,7,"coinanime",0.2],
    nocoinanime:[16,23,"nocoinanime",0.2]
    }
}
        let coinSheet = new createjs.SpriteSheet(coinData);
    if(coinGet==true){
                coinShow=new createjs.Sprite(coinSheet,"coinanime");
            }else if(coinGet==false){
                coinShow=new createjs.Sprite(coinSheet,"nocoinanime");
            }
         coinShow.x=400;
         coinShow.y=300;
         coinShow.scale=2;
         coinShow.alpha=0;
        if(currentLevel!=1){
         gameView.addChild(coinShow); 
        }
        
        leveltitle.text = "Level - "+currentLevel;
        leveltitle.x = 300;
        leveltitle.y = 200;
        leveltitle.alpha=0;
        gameView.addChild(leveltitle);
        setTimeout(showLeveltitle,500);
        function showLeveltitle(){                                  //show the level text and coin animation
        if(coinShow){
        createjs.Tween.get(coinShow).to({alpha:1},500)
        }
        createjs.Tween.get(leveltitle).to({alpha:1},500).call(hideLeveltitle);
  
        }
        
        function hideLeveltitle(){                                  //hidden level text and coin animation after a while
        if(coinShow){
        createjs.Tween.get(coinShow).wait(1000).to({alpha:0},500)
        }
        createjs.Tween.get(leveltitle).wait(1000).to({alpha:0},500).call(deleteLeveltitle); 
        }
        function deleteLeveltitle(){                                //remove level text and coin animation
            if(coinShow){
            gameView.removeChild(coinShow);
            }
            gameView.removeChild(leveltitle);
        }
    }
    setTimeout(restart,2500);
}else{
    setTimeout(credit,1500)                                     //when the next level is end, show end text and credit
    function credit(){
    gameStage.removeAllChildren();
    playerStage.removeAllChildren();
    objectStage.removeAllChildren();
    gameUI.removeAllChildren();
    debugContext.clearRect(-2000,-2000,4000,4000);  
    clearWorld();
    setup();
    }
}
}
}

function black(){                                       //black screen function
    isBlack=true;
    if(mapLevel==currentLevel){
     createjs.Tween.get(blackscreen).to({alpha:1},500).call(diedtextappear);
    }else {
    createjs.Tween.get(blackscreen).to({alpha:1},500);
    }
    mapLevel=currentLevel;
}

function white(){                                           //light up the screen
    
    createjs.Tween.get(blackscreen).to({alpha:0},500);
    
    if(currentLevel!=0&&currentLevel!=endLevel){
document.addEventListener("keydown",playerCharacter.statusChange);
document.addEventListener("keyup",playerCharacter.statusBack);   
       }
        isBlack=false;
}

function diedtextappear(){                                      //for dead text and restart text when player dead and screen come black
let diedtext = new createjs.Bitmap("sprites/died.PNG");
diedtext.alpha=0;
diedtext.y=150;
gameUI.addChild(diedtext);

let restarttext = new createjs.Bitmap("sprites/restart.PNG");
restarttext.alpha=0;
restarttext.y=350;
gameUI.addChild(restarttext); 
    
let restarttween=createjs.Tween.get(restarttext).to({alpha:1},1000).to({alpha:0},1000);
restarttween.loop=-1;
createjs.Tween.get(diedtext).to({alpha:1},1000);
setTimeout(addeventcheck,2500);
function addeventcheck(){                                           //add a enter pressing detect for restarting
    document.addEventListener("keydown",checkEnter);
}
function checkEnter(e){
    if(event.keyCode==13){
        document.removeEventListener("keydown",checkEnter);          //restart after player press enter
        createjs.Tween.get(diedtext).to({alpha:0},1000);
        createjs.Tween.get(restarttext).to({alpha:0},1000).call(restart);
    }
}    
}

