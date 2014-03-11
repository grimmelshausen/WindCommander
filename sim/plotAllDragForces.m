
close all

for i=[1:45:360]
    i
figure;  
ids = find(WindDir > i-0.4 & WindDir < i+0.4);
plot(DragForward(ids)+LiftForward(ids))
%set(gca,'XTick',SailAngle(ids));
end


