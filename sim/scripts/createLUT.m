% %create image and save
% clc
% close all
% 
% z = zi;
% 
% 
% z(isnan(z)) = 0;
% 
% z  = z/max((z(:)));
% imshow(z)
% imwrite(z, 'lut.png');

%
%create lut from interpolated sampled data zismu

clc
clear
close all

z = imread('lutHandPainted.png');
imshow(z);
z = im2double(z);
z  = z/max((z(:)));

A =  fileread('data/SpeedLUTTemplateA.txt');
B =  fileread('data/SpeedLUTTemplateB.txt');

disp('wait while writing file');
%write out LUT in C# code
fid = fopen('data/SpeedLUT.cs', 'w');

fprintf(fid, '%s', A);
for x=1:181 
    for y=1:91
        fprintf(fid, '%s\r\n\t\t', ['L[' num2str(x-1) ',' num2str(y-1) ']=' num2str(z(y,x)) 'f;']);
    end
end
fprintf(fid, '%s', B);
fclose(fid);

disp('ok');

