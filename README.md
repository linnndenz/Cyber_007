unity版本：2020.3.31f1c1  
渲染管线：URP  
插件使用：fungus dotween  

#### 音乐音效：
Manager为不销毁物体，BGM由Manager控制。  
音效由AudioManager控制，每个关卡继承这个类，重写InitSE。  
脚步声单独控制。  

#### 第三关
类UI的GameObject的渲染层放在单独的"UI"层  
有碰撞的物体Layer设置为UIItem，该层不与其他层物体产生物理  
迷宫墙的Layer设置为MazeWall，该层不与其他层物体产生物理  
tag里的Point和TextWall是黄金矿工小游戏用的  

