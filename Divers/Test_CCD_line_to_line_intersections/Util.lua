-- https://en.wikipedia.org/wiki/Line%E2%80%93line_intersection
function LineTolineIntersection(x1, y1, x2, y2, x3, y3, x4, y4)

    local denominator = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4)
    
    if denominator ~= 0 then
        
        local t = ((x1 - x3) * (y3 - y4) - (y1 - y3) * (x3 - x4)) / denominator
        
        if t >=0 and t <= 1 then
            
            local u = ((x1 - x3) * (y1 - y2) - (y1 - y3) * (x1 - x2)) / denominator
            
            if u >=0 and u <= 1 then
                local x = x1 + (t * (x2 - x1))
                local y = y1 + (t * (y2 - y1))
                return NewVector(x,y)
            end

        end

    end

end


function map(x, in_min, in_max, out_min, out_max)
    return out_min + (x - in_min)*(out_max - out_min)/(in_max - in_min)
end