var startbutton;
var buttontext;
var titletext;
var bgm

class titleScene extends Phaser.Scene{
    constructor(){
        super("gameTitle");
    }   
    
    preload(){
        this.load.image('sky','Resources/png/sky.png');
        this.load.image('startbutton','Resources/png/buttonDefault.png');
        this.load.image('startbuttonS','Resources/png/buttonSelected.png');
        this.load.audio('bgm','Resources/mp3/happy.mp3');
    }
    
    create(){
        this.add.image(400,300,'sky');
        titletext = this.add.text(400,100,"GEM PUZZLE",{fontFamily:'Arial'});
        titletext.setOrigin(0.5,0.5);
        titletext.align = 'center';
        titletext.setColor("#fff");
        titletext.setFontSize(100);
        
        startbutton = this.add.image(400,300,'startbutton');
        startbutton.setInteractive();
        startbutton.setOrigin(0.5,0.5);
        startbutton.on('pointerover',this.hoverButton);
        startbutton.on('pointerout',this.hoverOut);
        startbutton.on('pointerdown',() => this.scene.start("mainGame"));
        buttontext = this.add.text(400,300,"Start Game",{fontFamily:'Arial'});
        buttontext.setOrigin(0.5,0.5);
        buttontext.align = 'center';
        buttontext.setColor("#000");
        
        this.copyright = this.add.text(400,600,"Made By Xing",{fontFamily:'Arial'});
        this.copyright.setOrigin(0.5,1);
        this.copyright.align = 'center';
        
        bgm = this.sound.add('bgm');
        bgm.setLoop(true);
        bgm.play();
    }
    
    hoverButton(){
        startbutton.setTexture('startbuttonS');
        buttontext.setColor("red");
    }
    
    hoverOut(){
        startbutton.setTexture('startbutton');
        buttontext.setColor("#000");
    }
    
}