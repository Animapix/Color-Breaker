local columns = 16
local rows = 16
local brickSize = 32

local gridX = (love.graphics.getWidth()  - columns * brickSize) / 2
local gridY = (love.graphics.getHeight()  - rows * brickSize) / 2

local overflownColumn = 0
local overflownRow = 0

local selectedColor = 1;

local bricks = {}
local sides = { true, false, true, false }
local name = "noName"

function love.load()
    for column = 1, columns do
        bricks[column] = {}
        for row = 1, rows do
            bricks[column][row] = 0
        end
    end
end

function love.update()
    overflownColumn = math.floor((love.mouse.getX() - gridX) / brickSize) + 1
    overflownRow = math.floor((love.mouse.getY() - gridY) / brickSize) + 1
end

function love.mousepressed(x,y,btn)
    if overflownColumn <= 0 or overflownColumn > columns or overflownRow <= 0 or overflownRow > rows then return end
    if btn == 1 then
        bricks[overflownColumn][overflownRow] = selectedColor
    elseif btn == 2 then
        bricks[overflownColumn][overflownRow] = 0
    end
end

function love.keypressed(key)
    if key == "s" then
        saveFile()
    end

    if key == "up" then
        selectedColor = selectedColor  - 1
        if selectedColor < 1 then selectedColor = 4 end
    end

    if key == "down" then
        selectedColor = selectedColor + 1
        if selectedColor > 4 then selectedColor = 1 end

    end

    if key == "t" then
        sides[2] = not sides[2] 
    end
    if key == "b" then
        sides[4] = not sides[4] 
    end
    if key == "l" then
        sides[1] = not sides[1] 
        print(sides[1])
    end
    if key == "r" then
        sides[3] = not sides[3]
    end
end

function love.draw()
    drawGrid()
    drawWalls()
    for column = 1, columns do
        for row = 1, rows do
            local id = bricks[column][row]
            if id > 0 then
                if id == 1 then love.graphics.setColor(1,1,1,1)
                elseif id == 2 then love.graphics.setColor(1,0,0,1)
                elseif id == 3 then love.graphics.setColor(0,1,0,1)
                elseif id == 4 then love.graphics.setColor(0,0,1,1) end

                love.graphics.rectangle("fill", (column - 1) * brickSize + gridX, (row - 1) * brickSize + gridY, brickSize, brickSize)
            end
        end
    end
    drawOverflownCell(overflownColumn, overflownRow)
    drawColorSelector()
end

function drawOverflownCell(column, row)
    if column <= 0 or column > columns or row <= 0 or row > rows then return end
    love.graphics.setColor(0,0.4,1,0.5)
    love.graphics.rectangle("line", (column - 1) * brickSize + gridX, (row - 1) * brickSize + gridY, brickSize, brickSize)
    love.graphics.setColor(1,1,1,1)
end

function drawGrid()
    love.graphics.setColor(1,1,1,0.2)
    
    for i = 0, columns * brickSize, brickSize do
        love.graphics.line(gridX, i + gridY, columns * brickSize + gridX, i + gridY)
    end
    for i = 0, rows * brickSize, brickSize do
        love.graphics.line(i + gridX, gridY, i + gridX, rows * brickSize + gridY)
    end
    love.graphics.setColor(1,1,1,1)
end

function drawWalls()
    if sides[1] then
        love.graphics.rectangle("fill", gridX - 20, gridY - 20, 20,columns * brickSize + 40 )
    end
    if sides[2] then
        love.graphics.rectangle("fill", gridX - 20, gridY - 20, rows * brickSize + 40,20 )
    end
    if sides[3] then
        love.graphics.rectangle("fill", gridX + columns * brickSize, gridY - 20, 20,columns * brickSize + 40 )
    end
    if sides[4] then
        love.graphics.rectangle("fill", gridX - 20, rows * brickSize + 40, rows * brickSize + 40,20)
    end
end

function drawColorSelector()
    love.graphics.setColor(0,0.4,1,0.8)
    love.graphics.rectangle("fill", love.graphics.getWidth() - 100 - 4, 50 + (selectedColor - 1) * 60 -4, 58, 58)
    love.graphics.setColor(1,1,1,1)
    love.graphics.rectangle("fill",love.graphics.getWidth() - 100,50,50,50)
    love.graphics.setColor(1,0,0,1)
    love.graphics.rectangle("fill",love.graphics.getWidth() - 100,110,50,50)
    love.graphics.setColor(0,1,0,1)
    love.graphics.rectangle("fill",love.graphics.getWidth() - 100,170,50,50)
    love.graphics.setColor(0,0,1,1)
    love.graphics.rectangle("fill",love.graphics.getWidth() - 100,230,50,50)
    love.graphics.setColor(1,1,1,1)
end

function saveFile()
    json = require "json"
    DesktopDirectory = love.filesystem.getUserDirectory().."/Desktop/"
    file = io.open(DesktopDirectory .. 'Level.json', 'w')
    file:write(json.encode({name = name, sides = sides, bricks = bricks}))

    
    file:close()
end

