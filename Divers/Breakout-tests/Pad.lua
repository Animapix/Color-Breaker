function NewPad()
    local pad = {}
    pad.width = 150
    pad.height = 30
    pad.velocity = NewVector(0,0)
    pad.position = NewVector(love.graphics.getWidth()/2 - pad.width /2 ,love.graphics.getHeight() - 20 - pad.height)
    
    pad.draw = function()
        love.graphics.rectangle("line", pad.position.x, pad.position.y, pad.width, pad.height)
    end

    return pad
end