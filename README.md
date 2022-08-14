# Camera-response
Camera response demo.

默认 main scene 下运行，点击 scene 在 Shake Test 脚本下调整设置后按“空格”键查看效果。

        angle;                 // 逆时针 0 90 180 270，initial direction of the shake 
        strength;              // How far the shake can move the camera
        speed;                 // How fast the camera move during the shake
        duration;              // How long the shaking lasts 
        [Range(0,1)]
        noisePercent;          // Allow us to introduce some random variation of the shake
        [Range(0,1)]
        dampingPercent;       // How quickly of the shake decreases of the time, 圆圈的缩小速度，趋近于 0 匀速缩小；趋近于 1 刚开始很快，越往后越慢
        [Range(0,1)]
        rotationPercent;      // How much the movement of the shake affects camera's rotation