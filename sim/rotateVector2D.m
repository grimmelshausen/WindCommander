function [ vr ] = rotateVector2D( v, theta )
%Rotates v right theta degrees in deg
theta = -theta;

R = [cosd(theta) -sind(theta); sind(theta) cosd(theta)];
vr = v*R;
end

