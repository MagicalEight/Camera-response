# Camera-response
Camera response demo.

Ĭ�� main scene �����У���� scene �� Shake Test �ű��µ������ú󰴡��ո񡱼��鿴Ч����

        angle;                 // ��ʱ�� 0 90 180 270��initial direction of the shake 
        strength;              // How far the shake can move the camera
        speed;                 // How fast the camera move during the shake
        duration;              // How long the shaking lasts 
        [Range(0,1)]
        noisePercent;          // Allow us to introduce some random variation of the shake
        [Range(0,1)]
        dampingPercent;       // How quickly of the shake decreases of the time, ԲȦ����С�ٶȣ������� 0 ������С�������� 1 �տ�ʼ�ܿ죬Խ����Խ��
        [Range(0,1)]
        rotationPercent;      // How much the movement of the shake affects camera's rotation