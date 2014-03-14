close all
clear
clc

%scattered data interpolation
%input is :

% 6kn wind von 0 grad aus					
% 					
% heading	max speed kn	heel degree	leeway	in in irons	segel winkel geschätzt
% 360	0	1	0	yes	0
% 345	0	1	0	yes	0
% 340	0.9	1	4		3
% 320	2.1	2	4		10
% 308	2.6	2	4		
% 290	3	2	3		25
% 284	2.9	2	3		
% 270	2.9	1	3		
% 250	3	1	3		
% 238	2.3	0	2		45
% 222	2.2	0	2		
% 208	1.9	0	1		
% 193	1.8	0	0		
% 180	1.8	0	0		90
% 					
% 					
% 					
% 					
% 15kn wind  von 0 grad aus					
% 					
% heading	max speed kn	heel degree	leeway	in in irons	segel winkel geschätzt
% 360	0	1	0	yes	0
% 330	3.4	11	4		1
% 318	4.6	13	4		20
% 309	5.6	14	4		25
% 286	6.1	12	3		30
% 268	6.4	8	3		33
% 247	6.7	4	2		41
% 238	5.9	3	2		45
% 214	5.2	1	1		50
% 180	4.6	0	0		90
% 					
% 					
% 					
% 30kn wind  von 0 grad aus					
% 					
% heading	max speed kn	heel degree	leeway	in in irons	segel winkel geschätzt
% 360	0	1	0	yes	0
% 329	3.8	38	4		5
% 316	3.7	40	3		20
% 300	3.9	40	3		25
% 279	4.5	32	3		35
% 262	5.4	21	2		40
% 250	6	13	2		45
% 220	6.3	3	1		50
% 200	6.1	1	1		80
% 180	6.1	0	0		90



X = [6 %here 
6
6
6
6
6
6
6
6
6
6
6
6 
6 %until here 6kn
15 %here
15
15
15
15
15
15
15
15
15 %until here 15kn
30 %here 
30
30
30
30
30
30
30
30
30 %until here 30kn
]


Y = [360 %here 
345
340
320
308
290
284
270
250
238
222
208
193 
180 %until here 6kn
360 %here
330
318
309
286
268
247
238
214
180 %until here 15kn
360 %here 
329
316
300
279
262
250
220
200
180 %until here 30kn
]



    
    
    


%Z are ship speed in knots
Z = [0
0
0.9
2.1
2.6
3
2.9
2.9
3
2.3
2.2
1.9
1.8
1.8
0
3.4
4.6
5.6
6.1
6.4
6.7
5.9
5.2
4.6
0
3.8
3.7
3.9
4.5
5.4
6
6.3
6.1
6.1]
   
figure(1);
hold on
plot3(X, Y, zeros(length(X),1), '*r')
grid
title('Ship speed calculated from wind angle and wind speed using optimal main sail angle');
xlabel('Wind Speed [kn]'), ylabel('Wind Angle [deg]'), zlabel('Ship speed [kn]')
axis([0, 30, 180, 360, 0, 10]);
stem3(X,Y,Z,'^','fill')
view(322.5, 30);
hold off

figure(2);
hold on
t = delaunay(X,Y);
trisurf(t,X,Y, Z, 'FaceColor', [0.6875 0.8750 0.8984], 'FaceAlpha',0.9);
view(322.5, 30);
grid
title('Ship speed, DELAUNAY intep');
xlabel('Wind Speed [kn]'), ylabel('Wind Angle [deg]'), zlabel('Ship speed [kn]')
axis([0, 30, 180, 360, 0, 10]);
hold off


figure(3)
[xi, yi] = meshgrid(0:1:30, 180:2:360);
zi = griddata(X,Y,Z, xi,yi, 'linear');
surf(xi,yi,zi);
title('Ship speed, LINEAR interp');
xlabel('Wind Speed [kn]'), ylabel('Wind Angle [deg]'), zlabel('Ship speed [kn]')
axis([0, 30, 180, 360, 0, 10]);


figure(4)
[xi, yi] = meshgrid(0:1:30, 180:2:360);
zi = griddata(X,Y,Z, xi,yi, 'cubic');
surf(xi,yi,zi);
title('Ship speed, CUBIC interp');
xlabel('Wind Speed [kn]'), ylabel('Wind Angle [deg]'), zlabel('Ship speed [kn]')
axis([0, 30, 180, 360, 0, 10]);   
