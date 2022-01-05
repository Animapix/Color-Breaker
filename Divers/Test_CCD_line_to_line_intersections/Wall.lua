function NewWall(pointA, pointB )
    local wall = {}

    wall.pointA = pointA
    wall.pointB = pointB
    
    local wallMetaTable = {}

    function wallMetaTable.__tostring(w)
        return "("..w.pointA.x..","..w.pointA.y..") <=> ("..w.pointB.x..","..w.pointB.y..")"
    end

    setmetatable(wall,wallMetaTable)

    return wall
end