var canMove = false;
var startPosX=0;
var startPosY=0;
var tileWidth = 64;
var tileHeight = 64;
var activeTile1;
var activeTile2;
var hits = 0;

class playScene extends Phaser.Scene{
    
    constructor(){
        super("mainGame");

    }   
    
    preload(){
        this.load.image('blue','Resources/png/element_blue_diamond_glossy.png');
        this.load.image('green','Resources/png/element_green_polygon_glossy.png');
        this.load.image('grey','Resources/png/element_grey_square_glossy.png');
        this.load.image('purple','Resources/png/element_purple_diamond_glossy.png');
        this.load.image('red','Resources/png/element_red_polygon_glossy.png');
        this.load.image('yellow','Resources/png/element_yellow_square_glossy.png');
        this.load.spritesheet('cat','Resources/png/catrun.png',{ frameWidth: 88, frameHeight: 64 })
    }
    
    create(){
        this.add.image(400,300,'sky');
        
        this.tileTypes = [
            'blue',
            'green',
            'grey',
            'purple',
            'red',
            'yellow'   
        ]
        
        this.starAnimConf = (
        {key:'catAnim',
        frames:this.anims.generateFrameNumbers('cat',{start:0,end:6,first:0}),
        frameRate:10,
        repeat:-1
        }
        );
        
        this.anims.create(this.starAnimConf);
        this.add.sprite(680,300,'cat').play('catAnim');
        
        this.score=0;
        
        this.createScore();
        
        this.input.on('gameobjectdown',this.tileDown)
        
        this.tiles = this.add.group();
        
        this.tileGrid=[
            [null,null,null,null,null,null,null,null,null],
            [null,null,null,null,null,null,null,null,null],
            [null,null,null,null,null,null,null,null,null],
            [null,null,null,null,null,null,null,null,null],
            [null,null,null,null,null,null,null,null,null],
            [null,null,null,null,null,null,null,null,null],
            [null,null,null,null,null,null,null,null,null],
            [null,null,null,null,null,null,null,null,null],
            [null,null,null,null,null,null,null,null,null],
        ];
        
        this.random = Phaser.Math.RND;
        this.initTiles();
           
    }
    
    update(){
        var pointer = this.input.activePointer;
        
        if(activeTile1 && !activeTile2){          
            var hoverX = pointer.x;
            var hoverY = pointer.y;
             
            
            let hoverPosX = Math.floor(hoverX/tileWidth);
            let hoverPosY = Math.floor(hoverY/tileHeight);
            
            let difX = (hoverPosX-startPosX);
            let difY = (hoverPosY-startPosY);
            
            if(!(hoverPosY>this.tileGrid[0].length-1||hoverPosY<0)&& !(hoverPosX>this.tileGrid.length-1 || hoverPosX<0)){
                if((Math.abs(difY)==1 && difX==0)||(Math.abs(difX)==1&&difY==0)){
                    
                    canMove = false;
                    
                    activeTile2 = this.tileGrid[hoverPosX][hoverPosY];
                    
                    this.swapTiles(activeTile1,activeTile2);
                    
                    this.time.delayedCall(500, function(){
                        this.checkMatch();
                    },[],this);
                }
                
            }
        }
       
    }
    
    initTiles(){
        
        for(let i=0; i<this.tileGrid.length; i++){
            for(let j=0; j<this.tileGrid[i].length; j++){
                let tile = this.addTile(i,j);
                this.tileGrid[i][j]=tile;
            }   
        }
        this.time.delayedCall(600,function(){
            this.checkMatch();
        },[],this);
    }
    
    addTile(x,y){
    
    let tileToAdd = this.tileTypes[this.random.integerInRange(0,this.tileTypes.length-1)];
    
    let tile= this.tiles.create((x*tileWidth)+tileWidth/2,0,tileToAdd);
        
    this.tweens.add({targets:tile,y:y*tileHeight+(tileHeight/2), duration:500, ease:'Linear'});
        
    tile.setOrigin(0.5,0.5);
    
    tile.setInteractive();
    
    tile.tileType = tileToAdd;
    
    
    return tile;   
}
    
    tileDown(pointer,tile){
    if(canMove){
        hits = 0;
        activeTile1 = tile;
        startPosX = (tile.x-tileWidth/2)/tileWidth;
        startPosY = (tile.y-tileHeight/2)/tileHeight;  
    } 
}
    
    
    tileUp(){
    activeTile1 = null;
    activeTile2 = null;
}
 
    swapTiles(Tile1,Tile2){
    
    if(Tile1 && Tile2){
        
        let tile1Pos = {x:(Tile1.x-tileWidth/2)/tileWidth,y:(activeTile1.y-tileHeight/2)/tileHeight};
        let tile2Pos = {x:(Tile2.x-tileWidth/2)/tileWidth,y:(activeTile2.y-tileHeight/2)/tileHeight};
        
        this.tileGrid[tile1Pos.x][tile1Pos.y] = Tile2;
        this.tileGrid[tile2Pos.x][tile2Pos.y] = Tile1;
        
        this.tweens.add({targets:Tile1,x:tile2Pos.x*tileWidth+(tileWidth/2),y:tile2Pos.y*tileHeight+(tileHeight/2), duration:200, ease:'Linear'});
        this.tweens.add({targets:Tile2,x:tile1Pos.x*tileWidth+(tileWidth/2),y:tile1Pos.y*tileHeight+(tileHeight/2), duration:200, ease:'Linear'});
        
        activeTile1 = this.tileGrid[tile1Pos.x][tile1Pos.y];
        activeTile2 = this.tileGrid[tile2Pos.x][tile2Pos.y];
    }  
}
    
    checkMatch(){

    let matches = this.getMatches(this.tileGrid);
    
    if(matches.length >0){

        hits+=1;
        
        this.removeTileGroup(matches);
        
        this.resetTile();
        
        this.fillTile();
        
        this.time.delayedCall(500,function(){
            this.tileUp();
        },[],this);
        
        this.time.delayedCall(600,function(){
            this.checkMatch();
        },[],this);
    }else{
        this.swapTiles(activeTile1,activeTile2);
        this.time.delayedCall(500,function(){
            this.tileUp();
            canMove = true;
        },[],this);
    }    
}
    
    getMatches(tileGrid){
    let matches = [];
    let groups = [];
    
    for(let i=0; i < this.tileGrid.length; i++){
        let temp = this.tileGrid[i];
        groups=[];
        for(let j=0; j< temp.length; j++){
            if(j<temp.length-2){
                if(this.tileGrid[i][j]&&this.tileGrid[i][j+1]&&this.tileGrid[i][j+2]){
                    if(this.tileGrid[i][j].tileType==this.tileGrid[i][j+1].tileType&&this.tileGrid[i][j+1].tileType==this.tileGrid[i][j+2].tileType){
                        if(groups.length>0){
                            if(groups.indexOf(this.tileGrid[i][j])==-1){
                                matches.push(groups);
                                groups=[];
                            }
                        }
                        
                        if(groups.indexOf(this.tileGrid[i][j]==-1)){
                            groups.push(this.tileGrid[i][j]);
                        }
                        if(groups.indexOf(this.tileGrid[i][j+1]==-1)){
                            groups.push(this.tileGrid[i][j+1]);
                        }
                        if(groups.indexOf(this.tileGrid[i][j+2]==-1)){
                            groups.push(this.tileGrid[i][j+2]);
                        }
                    }
                }
            }         
        }
        if(groups.length>0){matches.push(groups);}
    }
    
    for(let j=0;j<this.tileGrid.length;j++){
        let temp = this.tileGrid[j];
        groups=[];
        for(let i=0;i<temp.length;i++){
            if(i<temp.length-2){
                if(this.tileGrid[i][j]&&this.tileGrid[i+1][j]&&this.tileGrid[i+2][j]){
                    if(this.tileGrid[i][j].tileType == this.tileGrid[i+1][j].tileType&&this.tileGrid[i+1][j].tileType == this.tileGrid[i+2][j].tileType){
                        if(groups.length>0){
                            if(groups.indexOf(this.tileGrid[i][j])==-1){
                                matches.push(groups);
                                groups = [];
                            }
                        }
                        
                        if(groups.indexOf(this.tileGrid[i][j]==-1)){
                            groups.push(this.tileGrid[i][j]);
                        }
                        if(groups.indexOf(this.tileGrid[i+1][j]==-1)){
                            groups.push(this.tileGrid[i+1][j]);
                        }
                        if(groups.indexOf(this.tileGrid[i+2][j]==-1)){
                            groups.push(this.tileGrid[i+2][j]);
                        }
                    }
                }
            }
            
        }
        if(groups.length>0){matches.push(groups);}
    }
    
    return matches;
}
    
    removeTileGroup(matches){
   
    for(let i=0; i<matches.length; i++){
        let temp = matches[i];
        for(let j=0;j<temp.length;j++){
            let tile = temp[j];
            let tilePos = this.getTilePos(this.tileGrid,tile);
            this.tiles.remove(tile,true,true);
            this.incrementScore();
            if(tilePos.x !=-1 && tilePos.y!=-1){
                this.tileGrid[tilePos.x][tilePos.y]=null;
            }
        }
    }
}
    
    getTilePos(tileGrid,tile){
    let pos = {x:-1,y:-1};
    for(let i=0;i<this.tileGrid.length;i++){
        for(let j=0;j<this.tileGrid[i].length;j++){
            if(tile==this.tileGrid[i][j]){
                pos.x=i;
                pos.y=j;
                break;
            }
        }
    }
    return pos;
}
    
    resetTile(){
        for(let i=0; i<this.tileGrid.length;i++){
            for(let j = this.tileGrid[i].length - 1; j>0; j--){
                if(this.tileGrid[i][j]==null&&this.tileGrid[i][j-1]!=null){
                    let tmpTile = this.tileGrid[i][j-1];
                    this.tileGrid[i][j]=tmpTile;
                    this.tileGrid[i][j-1]=null;
                    
                    this.tweens.add({targets:tmpTile,y:j*tileHeight+(tileHeight/2), duration:500, ease:'Linear'});
                    j=this.tileGrid[i].length;
                }
            }
        }
    }
    
    fillTile(){
        for(let i=0; i<this.tileGrid.length;i++){
            for(let j=0;j<this.tileGrid[i].length;j++){
                if(this.tileGrid[i][j]==null){
                    let tile = this.addTile(i,j);
                    this.tileGrid[i][j]=tile;
                    
                }
            }
        }
    }
    
    createScore(){
        this.scoreLabel = this.add.text(680, 100,"0",{fontFamily:'Arial'});
        this.scoreLabel.setOrigin(0.5,0);
        this.scoreLabel.align = 'center';
        this.scoreLabel.setFontSize(100);
        this.copyright = this.add.text(400,600,"Made By Xing",{fontFamily:'Arial'});
        this.copyright.setOrigin(0.5,1);
        this.copyright.align = 'center';
    }
    
    incrementScore(){
        this.score+=10*hits;
        this.scoreLabel.text = this.score;
    }
}