function NewBall(pX,pY)
    local ball = {}
    ball.position = NewVector(pX,pY)
    ball.velocity = NewVector(0,0)
    ball.radius = 20

    ball.draw = function()
        love.graphics.circle("line", ball.position.x, ball.position.y, ball.radius)
    end

    ball.update = function(dt, objects)
        

    end


    ball.checkCollisions = function(position, velocity , objects)

        local nextPosition = position + velocity

        local closest = {
            distance = 1e309
        }

        for i = 1, #objects do
            local box = objects[i]
            
            -- Check top
            local pt = LineTolineIntersection(
                position.x, 
                position.y,
                nextPosition.x,
                nextPosition.y,
                box.position.x - ball.radius,
                box.position.y - ball.radius,
                box.position.x + box.width + ball.radius,
                box.position.y - ball.radius
            )
            if pt ~= nil then
                local distance = pt.distance(position)
                if distance < closest.distance then
                    closest.side = "top"
                    closest.point = pt
                    closest.distance = distance
                end
            end

            -- Check bottom
            pt = LineTolineIntersection(
                position.x, 
                position.y,
                nextPosition.x,
                nextPosition.y,
                box.position.x - ball.radius,
                box.position.y + box.height + ball.radius,
                box.position.x + box.width + ball.radius,
                box.position.y + box.height + ball.radius
            )
            if pt ~= nil then
                local distance = pt.distance(position)
                if distance < closest.distance then
                    closest.side = "bottom"
                    closest.point = pt
                    closest.distance = distance
                end
            end

            -- Check left
            pt = LineTolineIntersection(
                position.x, 
                position.y,
                nextPosition.x,
                nextPosition.y,
                box.position.x - ball.radius,
                box.position.y - ball.radius,
                box.position.x - ball.radius,
                box.position.y + box.height + ball.radius
            )
            if pt ~= nil then
                local distance = pt.distance(position)
                if distance < closest.distance then
                    closest.side = "left"
                    closest.point = pt
                    closest.distance = distance
                end
            end

            -- Check right
            pt = LineTolineIntersection(
                position.x, 
                position.y,
                nextPosition.x,
                nextPosition.y,
                box.position.x + box.width + ball.radius,
                box.position.y - ball.radius,
                box.position.x + box.width + ball.radius,
                box.position.y + box.height + ball.radius
            )
            if pt ~= nil then
                local distance = pt.distance(position)
                if distance < closest.distance then
                    closest.side = "right"
                    closest.point = pt
                    closest.distance = distance
                end
            end
        end

        if closest.point ~= nil then
            
            velocity = velocity * (closest.distance / (nextPosition - position).norm())
            
            if closest.side == "top" or closest.side == "bottom" then
                velocity.y = -velocity.y
                ball.velocity.y = -ball.velocity.y
            elseif closest.side == "left" or closest.side == "right" then
                velocity.x = -velocity.x
                ball.velocity.x = -ball.velocity.x
            end
            local dir = NewVector(ball.velocity.x, ball.velocity.y).normalize() * 5
            print(dir)

            return ball.checkCollisions(closest.point + dir, velocity , objects)
        else
            return nextPosition
        end

        
    end

    ball.checkBounds = function(position, pWidth, pHeight)
        local newPosition = position

        if position.x + ball.radius > pWidth then
            ball.velocity.x = -ball.velocity.x
            newPosition.x = pWidth - ball.radius
        elseif position.x - ball.radius < 0 then
            ball.velocity.x = -ball.velocity.x
            newPosition.x = ball.radius
        end

        if position.y - ball.radius < 0 then
            ball.velocity.y = -ball.velocity.y
            newPosition.y = ball.radius
        end

        return newPosition
    end

    return ball
end