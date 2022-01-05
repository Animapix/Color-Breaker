function NewBrick(pX,pY)
    local brick = {}
    brick.width = 65
    brick.height = 65
    brick.position = NewVector(pX,pY)
    brick.free = false
    
    brick.draw = function()
        love.graphics.rectangle("line", brick.position.x, brick.position.y, brick.width, brick.height)
    end

    return brick
end