
close all
clear
clc

% app wind angle
AWA = [
0
20
27
50
80
100
180

0
20
27
50
80
100
180

0
20
27
50
80
100
180

0
20
27
50
80
100
180

0  
20
27
50
80
100
180
]

%true wind angle
TWA = [
0    
0
    0
    0
    0
    0
    0
    0
    
    5
    5
    5
    5
    5
    5
    
    8
    8
    8
    8
    8
    8
    8
    
    10
    10
    10
    10
    10
    10
    10
    
    12
    12
    12
    12
    12
    12
    12]

%boat speed in knots
BS = [
0
0
0
0
0
0
0

0
0
3.30
3.21
3.36
3.01
2.24

0
0
4.67
4.74
5.00
4.70
3.50

0
0
4.96
5.27
5.58
5.44
4.32

0
0
4.90
5.58
5.93
5.87
4.98
]
    
    

figure(1);
hold on
plot3(AWA, TWA, zeros(length(AWA),1), '*r')
grid
%title('Ship speed calculated from wind angle and wind speed using optimal main sail angle');
%xlabel('Wind Speed [kn]'), ylabel('Wind Angle [deg]'), zlabel('Ship speed [kn]')
axis([0, 180, 0, 15, 0, 10]);
stem3(AWA,TWA,BS,'^','fill')
view(322.5, 30);
hold off

figure(2)
[xi, yi] = meshgrid(0:1:180, 0:0.5:15);
zi = griddata(AWA,TWA,BS, xi,yi);
surf(xi,yi,zi);
title('Ship speed (for optimal sail angle), interp from VPP excel sheet');
xlabel('Apparent Wind Angle [deg]'), ylabel('True Wind Speed [kn]'), zlabel('Ship speed [kn]')
axis([0, 180, 0, 15, 0, 10]);

