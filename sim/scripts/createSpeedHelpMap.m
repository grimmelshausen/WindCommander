clear
clc
close all

I = imread('lutHandPainted.png');
I = im2double(I);
for i=1:180
    [m(i), j(i)] = max(I(:,i));
    %disp(['windAngle=' num2str(i) ' sailAngle=' num2str(j) ' speed=' num2str(m)]);
end

figure(1);
hold on;
for i=1:1:180
    line([0 sin(i/180*pi)*j(i)], [0 cos(i/180*pi)*j(i)]);
end