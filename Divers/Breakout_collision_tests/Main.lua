function love.load()
    box = {}
    box.x = 32
    box.y = 58
    box.w = 64
    box.h = 12

    ray = {}
    ray.x = 0
    ray.y = 0
    ray.vx = 2
    ray.vy = 2
    ray.moveSpeed = 100
    debug = "Test"

end



function love.update(dt)
    if love.keyboard.isDown("left") then
        ray.x = ray.x - ray.moveSpeed * dt
    end
    if love.keyboard.isDown("right") then
        ray.x = ray.x + ray.moveSpeed * dt
    end
    if love.keyboard.isDown("up") then
        ray.y = ray.y - ray.moveSpeed * dt
    end
    if love.keyboard.isDown("down") then
        ray.y = ray.y + ray.moveSpeed * dt
    end

    if deflx_ball_box(ray.x,ray.y,ray.vx,ray.vy,box.x,box.y,box.w,box.h) then
        debug = "horizontal"
    else
        debug = "vertical"
    end

end

function love.draw()
    love.graphics.rectangle("line", box.x, box.y, box.w, box.h)
    love.graphics.line(ray.x, ray.y, ray.x + ray.vx * 100, ray.y + ray.vy * 100)
    love.graphics.print(debug, 10,10)
end

function love.keypressed(key)
    if key == "a" then
        ray.vx = 2
        ray.vy = 2
    elseif key == "z" then
        ray.vx = -2
        ray.vy = 2
    elseif key == "e" then
        ray.vx = -2
        ray.vy = -2
    elseif key == "r" then
        ray.vx = 2
        ray.vy = -2
    end
end

function ballCollideBox(ballY, ballX, ballR, boxX, boxY, boxW, boxH)
    if ballY - ballR > boxY + boxH then
        return false
    end
    
    if ballY + ballR < boxY then
        return false
    end
    
    if ballX - ballR > boxX + boxW then
        return false
    end
    
    if ballX + ballR < boxX then
        return false
    end

    return true
end


function deflx_ball_box(BX,BY,BDX,BDY,RX,RY,RW,RH)
    if  BDX == 0 then 
        return false -- moving vert
    elseif BDY == 0 then 
        return true -- moving hor
    else
        local slp = BDY / BDX
        local cx, cy
        if slp > 0 and BDX > 0 then -- Ball moving down right
            cx = RX - BX
            cy = RY - BY
            if cx <= 0 then
                return false
            elseif cy / cx < slp then
                return true
            else
                return false
            end
        elseif slp < 0 and BDX > 0 then -- Ball moving up right
            cx = RX - BX
            cy = RY + RH - BY
            if cx <= 0 then
                return false
            elseif cy / cx < slp then
                return false
            else
                return true
            end
        elseif slp > 0 and BDX < 0 then -- Ball moving up left
            cx = RX + RW - BX
            cy = RY + RH - BY
            if cx >= 0 then
                return false
            elseif cy / cx > slp then
                return false
            else
                return true
            end
        else -- Ball moving down left
            cx = RX + RW - BX
            cy = RY - BY
            if cx >= 0 then
                return false
            elseif cy / cx < slp then
                return false
            else
                return true
            end
        end
    end
    return false
end