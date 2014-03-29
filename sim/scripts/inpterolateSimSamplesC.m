close all
clear
clc


run('data\dataSailAngleSampled')
run('data\dataShipSpeedSampled')
run('data\dataWindAngleSampled')
run('data\dataWindSpeedSampled')


%this is the third denser sampling with sandro on the phone

%scattered data interpolation

indices = find(WS==10.5);

X = WA(indices);
Y = SA(indices);
Z = SP(indices);


   
figure(1);
hold on
plot3(X, Y, zeros(length(X),1), '*r')
grid
title('Ship speed calculated from wind angle and main sail angle');
xlabel('Wind Angle [deg]'), ylabel('Sail Angle [deg]'), zlabel('Ship speed [kn]')
axis([0, 180, 0, 90, 0, 10]);
stem3(X,Y,Z,'^','fill')
view(322.5, 30);
hold off

% figure(2);
% hold on
% t = delaunay(X,Y);
% trisurf(t,X,Y, Z, 'FaceColor', [0.6875 0.8750 0.8984], 'FaceAlpha',0.9);
% view(322.5, 30);
% grid
% title('Ship speed, DELAUNAY intep');
% xlabel('Heading [deg]'), ylabel('Sail Angle [deg]'), zlabel('Ship speed [kn]')
% axis([0, 180, 0, 90, 0, 10]);
% hold off


% figure(3)
% [xi, yi] = meshgrid(0:1:180, 0:1:90);
% zi = griddata(X,Y,Z, xi,yi, 'linear');
% surf(xi,yi,zi);
% title('Ship speed, LINEAR interp');
% xlabel('Wind Angle [deg]'), ylabel('Sail Angle [deg]'), zlabel('Ship speed [kn]')
% axis([0, 180, 0, 90, 0, 10]);


% figure(4)
% [xi, yi] = meshgrid(0:1:180, 0:1:90);
% zi = griddata(X,Y,Z, xi,yi, 'cubic');
% surf(xi,yi,zi);
% title('Ship speed, CUBIC interp');
% xlabel('Wind Angle [deg]'), ylabel('Sail Angle [deg]'), zlabel('Ship speed [kn]')
% axis([0, 180, 0, 90, 0, 10]);



figure(5)
[xi, yi] = meshgrid(0:1:180, 0:1:90);
zi = griddata(X, Y ,Z, xi,yi, 'cubic');
H = fspecial('gaussian',[11 11], 500);
zismu = imfilter(zi, H, 'replicate');
zismu = imfilter(zismu, H, 'replicate');
zismu = imfilter(zismu, H, 'replicate');
surf(xi,yi,zismu);
title('Ship speed, SMOOTHED CUBIC interp');
xlabel('Wind Angle [deg]'), ylabel('Sail Angle [deg]'), zlabel('Ship speed [kn]')
axis([0, 180, 0, 90, 0, 10]);


% 
% disp('wait while writing file');
% %write out LUT in C# code
% fid = fopen('LUT.txt', 'w');
% fprintf(fid, '%s\r\n', 'float [,] L = new float[180,90];');
% for x=1:181 
%     for y=1:91
%         fprintf(fid, '%s\r\n', ['L[' num2str(x) ',' num2str(y) ']=' num2str(zismu(y,x)) 'f;']);
%     end
% end
% fclose(fid);
% disp('ok');



            
            


