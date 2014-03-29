clear
clc
close all

 h = waitbar(0,'Please wait...');
for iWA = 1:180
    waitbar(iWA/360,h)
    for iSA = 1:1:90

        %INPUT HERE
        sailAngle = iSA; %zero means the sail is all the way in, tigh. 90 means the sail is all open
        windAngle = iWA; %zero means the wind blows straight up, 90 means the wind blows to the right


        %calculate wind and sail vector from angle
        sailDir = [-1 0]; %sail direction is down (shit is looking up)
        sailDir = rotateVector2D(sailDir, sailAngle);
        wind = [-1 0];
        wind = rotateVector2D(wind, windAngle);
        sailDirR = rotateVector2D(sailDir, 90); %sailDirRight
        
        

        %drag force D
        %Project rel wind onto sail.right. The force is not applied at the hinge but at windMainSailForcePos, so you can move it around to make it look nice (higher up will make the ship tilt more)
        D = proj(wind, sailDirR);
        %Now project D on in ship direction because we need only this component
        sumD(iSA, iWA) = D(1) + 0.2*D(2);%since the boat is looking up we need only the x axis (same as project the drag force on boad direction)
        
        
        %blow sail
        B = norm(D); %the magnitude of the drag force is how much the sail is blown up
        sumB(iSA, iWA) = B;

        
        %heeling
        sumH(iSA, iWA) = -D(2);
        

        %Lift force
        %Project rel wind onto sail.forward for strength, but then apply force perpendicular
        sailDir = rotateVector2D(sailDir, 40);
        L = rotateVector2D(proj(wind, sailDir), 90) * (B)*5;
        sumL(iSA, iWA) = L(1);
        
        
        %in irons corrector
            if (sailAngle >= windAngle)
                sumB(iSA, iWA) = 0;
                sumD(iSA, iWA) = 0;
                sumL(iSA, iWA) = 0;
                sumH(iSA, iWA)= 0;
            end
        
     end

    %plot(sumD, 'r')
    %hold on
    %plot(sumL, 'g')
    %plot(sumL+sumD, 'b','LineWidth',3);
    %legend('Drag', 'Lift', 'Total Force')


end

close(h);

figure(1)
surf(sumD);
title('Drag');
ylabel('Sail Angle [deg]'), xlabel('Wind Angle [deg]'), zlabel('Force')

figure(2);
surf(sumL);
title('Lift');
ylabel('Sail Angle [deg]'), xlabel('Wind Angle [deg]'), zlabel('Force')

figure(3);
surf(sumL+sumD);
title('Total Lift+Drag');
ylabel('Sail Angle [deg]'), xlabel('Wind Angle [deg]'), zlabel('Force')

figure(4);
surf(sumB);
title('Blow');
ylabel('Sail Angle [deg]'), xlabel('Wind Angle [deg]'), zlabel('Force')


figure(5);
surf(sumH);
title('Heel');
ylabel('Sail Angle [deg]'), xlabel('Wind Angle [deg]'), zlabel('Force')
            