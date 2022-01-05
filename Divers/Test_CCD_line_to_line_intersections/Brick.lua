



function NewBrick(pX, pY)
    local brick = {}

    brick.position = NewVector(pX,pY)
    brick.width = 65
    brick.height = 65

    brick.draw = function()
        love.graphics.rectangle("line",brick.position.x, brick.position.y, brick.width, brick.height)
        love.graphics.line(brick.position.x, brick.position.y - 20, brick.position.x + brick.width, brick.position.y - 20)
    end

    brick.CheckCollision = function(currentX, currentY, nextX, nextY, radius)

        local pt = LineTolineIntersection(
            currentX, 
            currentY, 
            nextX, 
            nextY, 
            brick.position.x, 
            brick.position.y - radius, 
            brick.position.x + brick.width, 
            brick.position.y - radius
        )

        print(pt)

    end

    return brick
end