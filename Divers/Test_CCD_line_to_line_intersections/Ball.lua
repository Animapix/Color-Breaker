

function NewBall(pX,pY)
    local ball = {}

    ball.position = NewVector(pX,pY)
    ball.velocity = NewVector(0,0)
    ball.radius = 20
    ball.speed = 50

    

    ball.launch = function(dir)
        ball.velocity = dir.normalize() * ball.speed
    end

    ball.nextPosition = function(vel, dt)
        return ball.position + vel * dt
    end

    ball.update = function(dt, walls)
        
        local nextPosition = ball.nextPosition(ball.velocity, dt)

        local closest = {
            distance = 1e309
        }

        for i = 1, #walls do
            local wall = walls[i]
            local pt = LineTolineIntersection(
                ball.position.x, 
                ball.position.y,
                nextPosition.x,
                nextPosition.y,
                wall.pointA.x,
                wall.pointA.y,
                wall.pointB.x,
                wall.pointB.y
            )
            if pt ~= nil then
                local distance = pt.distance(ball.position)
                if distance < closest.distance then
                    closest.wall = wall
                    closest.point = pt
                    closest.distance = distance
                end
            end
        end
        if closest.point ~= nil then
            ball.position = closest.point
            ball.velocity.y = -ball.velocity.y
            local udt = dt * (closest.distance / (nextPosition - ball.position).norm())
            ball.update(dt - udt, walls)
        else
            ball.position = nextPosition
        end

        --ball.CheckBounds(love.graphics.getWidth( ),love.graphics.getHeight( ));

    end

    ball.checkCollisions = function(nextPosition, walls)

        local closest = nil
        local closestDistance = 1e309
        for i = 1, #walls do
            local pt = LineTolineIntersection(
                ball.position.x, 
                ball.position.y,
                nextPosition.x,
                nextPosition.y,
                walls[i].pointA.x,
                walls[i].pointA.y,
                walls[i].pointB.x,
                walls[i].pointB.y
            )
            if pt ~= nil then
                local distance = pt.distance(ball.position)
                if distance < closestDistance then
                    closest = {
                        wall = walls[i],
                        point = pt,
                        distance = distance
                    }
                end
            end
        end

        if closest ~= nil then
            print("collision", closest.point)
            ball.velocity.y = -ball.velocity.y
            ball.checkCollisions(closest.point, walls)
        else
            return nextPosition
        end
    end

    ball.draw = function()
        love.graphics.circle("line", ball.position.x,ball.position.y,ball.radius);
    end

    ball.CheckBounds = function(pWidth, pHeight)
        if ball.position.x + ball.radius > pWidth then
            ball.velocity.x = -ball.velocity.x
            ball.position.x = pWidth - ball.radius
        elseif ball.position.x - ball.radius < 0 then
            ball.velocity.x = -ball.velocity.x
            ball.position.x = ball.radius
        end

        if ball.position.y + ball.radius > pHeight then
            ball.velocity.y = -ball.velocity.y
            ball.position.y = pHeight - ball.radius
        elseif ball.position.y - ball.radius < 0 then
            ball.velocity.y = -ball.velocity.y
            ball.position.y = ball.radius
        end
    end

    return ball
end