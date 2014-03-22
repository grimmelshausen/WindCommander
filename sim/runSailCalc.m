
clear
clc
close  all
for AWS=1:30
for AWA=1:180
    [DriveResult, HeelResult] = sailCalc(AWS, AWA, 0);
    drive(AWA, AWS) = DriveResult;
    heel(AWA, AWS) = HeelResult;
end
end

figure(1);
plot(drive);

title('Sail Force calculated for different wind speeds');
xlabel('Apparent Wind Angle')
ylabel('Force')


figure(2);
plot(heel);

title('Heel calculated for different wind speeds');
xlabel('Apparent Wind Angle')
ylabel('Force')