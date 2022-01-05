require "Util"
require "Vector"

local direction = NewVector(0,0)

local ball = {}
ball.position = NewVector(100,100)
ball.velocity = NewVector(0,0)
ball.radius = 20
ball.next = NewVector(0,0)


local walls = {
    {a = NewVector(200,300), b = NewVector(400,300)},
    {a = NewVector(50,320), b = NewVector(400,320)},
    {a = NewVector(400,300), b = NewVector(400,50)}
}

local rays = {}
local closest = { distance = 1e309}
local collisionPoints = {}

function love.load()
end

function checkCollisions(pCurrentPosition, pVelocity, pWalls)

    print(pCurrentPosition,pVelocity)
    rays = {}
    closest = {}
    closest.distance = 1e309
    



    for i=1, #pWalls do
        local wall = pWalls[i]
        local pt = LineTolineIntersection(
            pCurrentPosition.x, 
            pCurrentPosition.y, 
            pCurrentPosition.x + pVelocity.x, 
            pCurrentPosition.y + pVelocity.y, 
            wall.a.x, 
            wall.a.y, 
            wall.b.x, 
            wall.b.y
        )
        if pt ~= nil then
            local distance = pt.distance(pCurrentPosition)
            if distance < closest.distance then
                closest.distance = distance
                closest.point = pt
                closest.wall = wall
            end
            
        end
    end

    

    if closest.point ~= nil then
        table.insert(collisionPoints, closest.point)
        local velocityNorm = pVelocity.norm()
        local dir = NewVector(pVelocity.x, pVelocity.y)
        dir.normalize()
        local distance = pCurrentPosition.distance(closest.point)
        local newVel = dir * (velocityNorm - distance)
       
        local horizontal = (closest.wall.b.y - closest.wall.a.y) / (closest.wall.b.x - closest.wall.a.x) == 0

        if horizontal then
            newVel.y = -newVel.y
        else
            newVel.x = -newVel.x
        end
        
        return checkCollisions(closest.point - dir, newVel, pWalls)
    else
        return pCurrentPosition + pVelocity
    end

end
 
function love.update(dt)
    ball.velocity.x = love.mouse.getX() - ball.position.x
    ball.velocity.y = love.mouse.getY() - ball.position.y
    collisionPoints = {}
    ball.next =  checkCollisions(ball.position, ball.velocity, walls)
end


function love.draw()

    drawWalls()
    drawCollisionPoints()  
    drawBall()

end

function drawBall()
    love.graphics.line(ball.position.x,ball.position.y,ball.position.x + ball.velocity.x,ball.position.y + ball.velocity.y)
    love.graphics.circle("line", ball.position.x, ball.position.y, ball.radius)
    if ball.next ~= nil then
        love.graphics.circle("line", ball.next.x, ball.next.y, ball.radius)
    end
end

function drawWalls()
    for i=1, #walls do
        local wall = walls[i]
        love.graphics.line(wall.a.x, wall.a.y, wall.b.x, wall.b.y)
    
    end



end

function drawCollisionPoints()
    love.graphics.setColor(1,0,0)
    local points = {ball.position.x,ball.position.y}

    for i=1, #collisionPoints do
        local point = collisionPoints[i]
        love.graphics.circle("fill", point.x, point.y, 3)
        table.insert(points, point.x)
        table.insert(points, point.y)
    end
    table.insert(points, ball.next.x)
    table.insert(points, ball.next.y)
    if #points > 4 then
        love.graphics.line(points)
    end
    love.graphics.setColor(1,1,1)

end
