close all
clear
clc

%scattered data interpolation


%heading
X = [45
45
45
45
90
90
90
90
90
90
90
90
135
135
135
135
135
135
135

180
180
180
180
180
180]

%segel winkel
Y = [0
    20
    40
    42
    0
    10
    25
    30
    45
    55
    70
    72
  0
  10
  20
  30
  45
  60
  90
  21
  25
  45
  60
  70
  90]   

   

%Z are ship speed in knots
Z = [3.6
    4.1
    4
    0
    3.7
    3.9
    4.4
    4.9
    5.1
    4.8
    4.4
    0
    1.8
    3.3
    3.6
    3.8
    4.3
    4.7
    4
    0
    2.6
    2.9
    3.3
    3.6
    3.9]
   
figure(1);
hold on
plot3(X, Y, zeros(length(X),1), '*r')
grid
title('Ship speed calculated from wind angle and main sail angle');
xlabel('Heading [deg]'), ylabel('Sail Angle [deg]'), zlabel('Ship speed [kn]')
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


figure(3)
[xi, yi] = meshgrid(0:1:180, 0:1:90);
zi = griddata(X,Y,Z, xi,yi, 'linear');
surf(xi,yi,zi);
title('Ship speed, LINEAR interp');
xlabel('Heading [deg]'), ylabel('Sail Angle [deg]'), zlabel('Ship speed [kn]')
axis([0, 180, 0, 90, 0, 10]);


figure(4)
[xi, yi] = meshgrid(0:1:180, 0:1:90);
zi = griddata(X,Y,Z, xi,yi, 'cubic');
surf(xi,yi,zi);
title('Ship speed, CUBIC interp');
xlabel('Heading [deg]'), ylabel('Sail Angle [deg]'), zlabel('Ship speed [kn]')
axis([0, 180, 0, 90, 0, 10]);
