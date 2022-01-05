require 'Vector'
require 'Ball'
require 'Pad'
require 'Brick'



function love.load()
    ball = NewBall(100,100)
    ball.velocity = NewVector(100,150) * 3
    pad = NewPad()

    bricks = {}
    for column = 65, 65*10, 65 + 10 do
        for row = 65, 65*5, 65 + 10 do
            table.insert(bricks,NewBrick(column,row))
        end
    end

end

local fall = false

function love.update(dt)
    local nextPosition = ball.position 
    local nextPadPosition = pad.position 

    local btnPressed = false
    if love.keyboard.isDown("right") then
        pad.velocity.x = 500
        btnPressed = true
    end
    if love.keyboard.isDown("left") then
        pad.velocity.x = -500
        btnPressed = true
    end
    if not btnPressed then
        pad.velocity.x = pad.velocity.x * 0.95
    end
    
    local dtDiv = dt/50

    for i = 50, 1, -1 do
        nextPosition = nextPosition + ball.velocity * dtDiv
        nextPosition = ball.checkBounds(nextPosition, love.graphics.getWidth(), love.graphics.getHeight());

        if nextPosition.y + ball.radius > love.graphics.getHeight() + 10 then
            nextPosition = NewVector(100,100)
            ball.velocity = NewVector(100,150) * 3
            fall = false;
        end

        local collide = IsIntersected(nextPosition, ball.radius, nextPadPosition, pad.width, pad.height)
        if collide then
            
            if deflx_ball_box(nextPosition.x,nextPosition.y,ball.velocity.x,ball.velocity.y,nextPadPosition.x,nextPadPosition.y,pad.width,pad.height) then
                ball.velocity.x = -ball.velocity.x
                fall = true
            else
                if not fall then
                    ball.velocity.y = -ball.velocity.y
                end
            end
        end
        
        for i=#bricks,1, -1  do
            collide = IsIntersected(nextPosition, ball.radius, bricks[i].position, bricks[i].width, bricks[i].height)
            if collide then
                if deflx_ball_box(nextPosition.x,nextPosition.y,ball.velocity.x,ball.velocity.y,bricks[i].position.x,bricks[i].position.y,bricks[i].width,bricks[i].height) then
                    ball.velocity.x = -ball.velocity.x
                else
                    ball.velocity.y = -ball.velocity.y
                end
                table.remove(bricks,i)
                break
            end
        end

        nextPadPosition = nextPadPosition + pad.velocity * dtDiv
    end
   


    ball.position = nextPosition
    pad.position = nextPadPosition
end

function love.draw()
    ball.draw()
    pad.draw()

    for i=1, #bricks do
        bricks[i].draw()
    end
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

function IsIntersected(circlePosition, circleRadius, rectanglePosition, rectangleWidth, rectangleHeight)
    if circlePosition.y - circleRadius > rectanglePosition.y + rectangleHeight then
		return false
	end
	
	if circlePosition.y + circleRadius < rectanglePosition.y then
		return false
	end
	
	if circlePosition.x - circleRadius > rectanglePosition.x + rectangleWidth then
		return false
	end
	
	if circlePosition.x + circleRadius < rectanglePosition.x then
		return false
	end
	
	
	
	return true
end