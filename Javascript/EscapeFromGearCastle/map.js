"use strict"
let tilesarray=[];
let tilesid=0;
let objectsarray=[];
let level1objects=[];
let level2objects=[];
let level3objects=[];
let startpoint=[];
startpoint[0]=null;         //it's start point of character for each level
startpoint[1]=[150,370];
startpoint[2]=[150,370];
startpoint[3]=[150,350];

function drawMap(stage,levelnum){              //draw the map according to each tile's type and position in map array. and restore drawn tiles list in another array for level rotation
    let level;
    if(levelnum==1){level=level1map
    }else if(levelnum==2){level=level2map
    }else if(levelnum==3){level=level3map};
    
    for(let i=0;i<level.length;i++){
        for(let j=0;j<level[i].length;j++){
            if(level[i][j]!=0){
            tilesarray[tilesid] = new tiles(stage,level[i][j],j,i);
            tilesarray[tilesid].createTile(stage,level[i][j],j,i);
            tilesid+=1;
            }
        }
    }
    tilesid=0;
    return levelnum
}

function drawObjects(stage,levelnum){       //draw the interactive objects for each level and restore objects list in another array for level rotation
    
    if(levelnum==1){objectsarray=level1objects
        }else if(levelnum==2){objectsarray=level2objects
        }else if(levelnum==3){objectsarray=level3objects
        }
    
    for(let i=0;i<objectsarray.length;i++){
        objectsarray[i].createObject();
    }
    return levelnum
}

function rotateAll(anchorx,anchory){     //rotating all tiles and objects

    rotateMap(anchorx,anchory);
    rotateObjects(anchorx,anchory);
    rotationDirection=0;
    setTimeout(changetypeBack,2000)
}

function changetypeBack(){                  //change the body type of falling heavey objects back from kinematic to dynamic after rotating
             for(let i=0;i<objectsarray.length;i++){ 
                 if((objectsarray[i].type==5||objectsarray[i].type==6||objectsarray[i].type==7||objectsarray[i].type==8)&&objectsarray[i].triggered==true){
                 objectsarray[i].body.SetType(2);
             }
         }
}

function rotateObjects(anchorx,anchory){     // for rotating objects

    
    for(let i=0;i<objectsarray.length;i++){
        
            if(rotationDirection==1&&objectsarray[i].type!=9&&objectsarray[i].type!=10){       //clockwise, normal objects
           let x0=objectsarray[i].image.x-anchorx;
           let y0=objectsarray[i].image.y-anchory;
           let x1=-y0+anchorx;
           let y1=x0+anchory;
           let rotation1=objectsarray[i].image.rotation+90;
           createjs.Tween.get(objectsarray[i].image,{override:true}).to({x:x1,y:y1,rotation:rotation1},2000);

       }else if(rotationDirection==-1&&objectsarray[i].type!=9&&objectsarray[i].type!=10){    //counterclockwise, normal objects
           let x0=objectsarray[i].image.x-anchorx;
           let y0=objectsarray[i].image.y-anchory;
           let x1=y0+anchorx;
           let y1=-x0+anchory;
           let rotation1=objectsarray[i].image.rotation-90;
           createjs.Tween.get(objectsarray[i].image,{override:true}).to({x:x1,y:y1,rotation:rotation1},2000);  

       }else  if(rotationDirection==1&&objectsarray[i].type==9){        //clockwise, non-rotation anchor objects
           let x0=objectsarray[i].image.x-anchorx;
           let y0=objectsarray[i].image.y-anchory;
           let x1=-y0+anchorx;
           let y1=x0+anchory;
           //let rotation1=objectsarray[i].image.rotation+90;
           createjs.Tween.get(objectsarray[i].image,{override:true}).to({x:x1,y:y1},2000);
           
        for(let i=0;i<objectsarray.length;i++){                         //clockwise, non-rotation objects
           if(objectsarray[i].type==10){
           let x2=objectsarray[i].image.x-anchorx;
           let y2=objectsarray[i].image.y-anchory;
           let x3=-y2+anchorx;
           let y3=x2+anchory;
           let x4=x3-x1;
           let y4=y3-y1;
           let x5=y4+x1
           let y5=-x4+y1
           //let rotation1=objectsarray[i].image.rotation+90;
           createjs.Tween.get(objectsarray[i].image,{override:true}).to({x:x5,y:y5},2000);

       }
        }
       }else if(rotationDirection==-1&&objectsarray[i].type==9){            //counterclockwise, non-rotation anchor objects
           let x0=objectsarray[i].image.x-anchorx;
           let y0=objectsarray[i].image.y-anchory;
           let x1=y0+anchorx;
           let y1=-x0+anchory;
           //let rotation1=objectsarray[i].image.rotation-90;
           createjs.Tween.get(objectsarray[i].image,{override:true}).to({x:x1,y:y1},2000);  
                   for(let i=0;i<objectsarray.length;i++){
           if(objectsarray[i].type==10){                                    //counterclockwise, non-rotation objects
           let x2=objectsarray[i].image.x-anchorx;
           let y2=objectsarray[i].image.y-anchory;
           let x3=y2+anchorx;
           let y3=-x2+anchory;
           let x4=x3-x1;
           let y4=y3-y1;
           let x5=-y4+x1;
           let y5=x4+y1
           //let rotation1=objectsarray[i].image.rotation+90;
           createjs.Tween.get(objectsarray[i].image,{override:true}).to({x:x5,y:y5},2000);

       }
        }
           
       }
    }
}

function rotateMap(anchorx,anchory) {                           //level rotation
    for (let i=0; i<tilesarray.length;i++){
       if(rotationDirection==1){                                //clockwise
           let x0=tilesarray[i].image2.x-anchorx;
           let y0=tilesarray[i].image2.y-anchory;
           let x1=-y0+anchorx;
           let y1=x0+anchory;
           let rotation1=tilesarray[i].image2.rotation+90;
           createjs.Tween.get(tilesarray[i].image2,{override:true}).to({x:x1,y:y1,rotation:rotation1},2000);

       }else if(rotationDirection==-1){                         //counterclockwise
           let x0=tilesarray[i].image2.x-anchorx;
           let y0=tilesarray[i].image2.y-anchory;
           let x1=y0+anchorx;
           let y1=-x0+anchory;
           let rotation1=tilesarray[i].image2.rotation-90;
           createjs.Tween.get(tilesarray[i].image2,{override:true}).to({x:x1,y:y1,rotation:rotation1},2000);  

       }
    }   
}



let level1map = [                                                   //level 1 map
    [2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2],
    [2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2],
    [2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2],
    [2,2,2,1,1,1,1,1,1,1,1,1,1,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,15,2,2,2],
    [2,2,2,1,1,1,1,1,1,1,1,1,1,2,1,1,1,1,1,1,1,1,1,3,2,2,2,2,2,2,2,2,2],
    [2,2,2,1,0,0,1,1,1,0,0,1,1,2,0,0,1,1,1,0,0,1,3,5,0,0,1,1,1,1,2,2,2],
    [2,2,2,1,0,0,1,1,1,0,0,1,1,2,0,0,1,1,1,2,2,2,5,1,0,0,1,1,1,1,2,2,2],
    [2,2,2,1,1,1,1,1,1,1,1,1,1,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,2,2],
    [2,2,2,1,1,1,1,1,1,1,1,1,1,2,2,2,10,9,1,1,1,1,1,1,1,1,1,1,1,1,2,2,2],
    [2,2,2,1,1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2,2,4,1,1,1,1,1,1,1,2,2,2],
    [2,2,2,1,0,0,1,1,1,0,0,1,1,1,0,0,1,2,1,0,0,2,2,1,0,0,1,1,1,1,2,2,2],
    [2,2,2,1,0,0,1,1,1,0,0,1,1,1,0,0,1,2,1,0,0,1,1,1,0,0,1,1,1,1,2,2,2],
    [2,2,2,1,1,1,1,1,1,1,1,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,2,2],
    [2,2,2,1,16,1,1,1,1,1,1,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,7,8,2,2,2,2,2],
    [2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2],
    [2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2],
    [2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2]

];

let level2map = [                                               //level 2 map
    [2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2],
    [2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2],
    [2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2],
    [2,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,2,2,2],
    [2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,2,2],
    [2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,2,2],
    [2,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,2,2],
    [2,2,2,15,1,1,1,1,1,17,17,17,17,17,17,17,2,2,2,2,2,2,2,2,2,1,1,1,1,1,1,1,1,2,2,2],
    [2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,18,1,1,1,1,1,1,1,2,2,2],
    [2,2,2,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,19,1,1,1,1,1,1,1,1,2,2,2],
    [2,2,18,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,20,2,2,1,1,1,1,2,2,2],
    [2,2,18,1,0,0,1,1,1,0,0,1,1,1,0,0,1,1,1,0,0,1,1,1,0,0,1,1,1,1,1,1,1,20,2,2],
    [2,2,18,1,0,0,1,1,1,0,0,1,1,1,0,0,1,1,1,0,0,1,1,1,0,0,1,1,1,1,1,1,1,20,2,2],
    [2,2,18,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,20,2,2,1,1,2,2,2],
    [2,2,18,1,16,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,20,2,2],
    [2,2,2,2,2,2,2,2,2,17,2,2,17,17,2,2,17,17,17,2,2,17,17,17,17,2,2,2,2,2,2,2,2,2,2,2],
    [2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2],
    [2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2],

];

let level3map=[                                                 //level 3 map
    [2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2],
    [2,2,2,2,2,2,2,2,2,1,1,2,2,2,2,2,1,1,2,2,2,2,2,2,2,2,2,1,1,2,2,2,2],
    [2,2,2,19,19,19,19,2,2,1,1,2,2,19,2,2,1,1,2,2,19,19,19,19,19,2,2,1,1,2,2,2,2],
    [2,2,18,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,20,2,2],
    [2,2,18,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,20,2,2],
    [2,2,18,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,20,2,2],
    [2,2,18,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,20,2,2],
    [2,2,18,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,20,2,2],
    [2,2,18,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,20,2,2],
    [2,2,18,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,20,2,2],
    [2,2,18,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,2,2],
    [2,2,18,1,16,1,1,1,1,1,1,1,1,1,1,1,1,1,2,2,2,2,2,1,1,1,2,1,1,1,2,2,2],
    [2,2,2,2,2,2,2,17,2,2,2,2,2,17,17,2,2,2,2,2,2,2,1,1,1,1,2,2,2,2,2,2,2],
    [2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,18,1,1,1,1,20,2,2,2,2,2,2],
    [2,2,2,2,2,2,2,2,2,19,19,19,2,2,2,2,2,2,2,2,2,18,1,1,1,1,20,2,2,2,2,2,2],
    [0,0,2,2,2,14,13,1,1,1,1,1,1,1,11,12,2,2,2,2,2,18,1,1,1,1,20,2,2,0,0,0],
    [0,2,2,2,5,1,1,1,1,1,1,1,1,1,1,1,6,2,2,2,2,18,1,1,1,1,20,2,2,0,0,0,0],
    [2,2,2,5,1,1,1,1,1,1,1,1,1,1,1,1,1,6,2,2,2,18,1,1,1,1,20,2,2,0,0,0,0],
    [2,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,2,2,18,1,1,1,1,20,2,2,0,0,0,0],
    [2,2,2,1,1,1,1,1,1,7,8,2,10,9,1,1,1,1,20,2,2,18,1,1,1,1,20,2,2,0,0,0,0],
    [2,2,2,1,1,1,1,1,3,2,2,2,2,2,4,1,1,1,2,2,2,18,1,1,1,1,20,2,2,0,0,0,0],
    [2,2,2,1,1,1,1,3,2,2,5,15,6,2,2,1,1,1,2,2,2,18,1,1,1,1,20,2,2,0,0,0,0],
    [2,2,18,1,1,1,1,2,2,18,1,1,1,1,1,1,1,1,2,2,2,18,1,1,1,1,20,2,2,0,0,0,0],
    [2,2,18,1,1,1,1,2,2,2,1,1,1,1,1,1,1,1,2,2,2,2,1,1,1,1,2,2,2,0,0,0,0],
    [2,2,18,1,1,1,1,2,2,2,4,1,1,1,1,1,1,3,2,2,2,2,1,1,1,1,2,2,2,0,0,0,0],
    [2,2,2,1,1,1,1,6,2,2,2,2,2,2,2,2,2,2,2,2,2,5,1,1,1,1,2,2,2,0,0,0,0],
    [2,2,2,1,1,1,1,1,6,2,2,2,2,2,2,2,2,2,2,2,5,1,1,1,1,1,2,2,2,0,0,0,0],
    [2,2,2,1,1,1,1,1,1,11,12,2,2,2,2,2,2,2,14,13,1,1,1,1,1,1,2,2,2,0,0,0,0],
    [2,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,2,2,0,0,0,0],
    [2,2,2,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,2,2,2,0,0,0,0],
    [0,2,2,2,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,2,2,2,2,0,0,0,0],
    [0,0,2,2,2,10,9,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,7,8,2,2,2,2,2,0,0,0,0],
    [0,0,0,2,2,2,2,2,2,2,2,2,2,17,17,17,2,2,2,2,2,2,2,2,2,2,2,2,0,0,0,0,0],
    [0,0,0,0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0,0,0,0,0,0],
    [0,0,0,0,0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0,0,0,0,0,0],
];


function loadObjects(){                                                 //objects for all 3 level;
level1objects[0]= new objects(objectStage,1,5,13);
level1objects[1]= new objects(objectStage,2,8,13);
level1objects[2]= new objects(objectStage,4,29,12);
level1objects[3]= new objects(objectStage,11,11,4);

level2objects[0]= new objects(objectStage,3,6,14);
level2objects[1]= new objects(objectStage,8,1,4,31);
level2objects[2]= new objects(objectStage,11,32,4);
    
level3objects[0] = new objects(objectStage,9,15,22);
level3objects[1] = new objects(objectStage,10,15,21); 
level3objects[2] = new objects(objectStage,10,15,20); 
level3objects[3] = new objects(objectStage,10,15,23); 
level3objects[4] = new objects(objectStage,10,15,24); 
level3objects[5] = new objects(objectStage,11,29,10);
level3objects[6] = new objects(objectStage,5,27,1,10);
level3objects[7] = new objects(objectStage,5,9,1,10);
level3objects[8] = new objects(objectStage,5,16,1,10);
    
}
