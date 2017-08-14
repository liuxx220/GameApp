/*
 * ----------------------------------------------------------------------------------------------
 *          file name : Grid.cs 
 *          desc      : 地形Grid
 *          author    : 李江坡
 *          log       : by ljp 创建 [ 2015-12-27 
 *                    : 三维地形映射到2D维度块
 * ----------------------------------------------------------------------------------------------         
*/
using UnityEngine;
using System.Collections;



public class CGrid
{

    /// -----------------------------------------------------------------------------------------
    /// <summary>
    /// 地形 patch 的四叉树
    /// </summary>
    /// -----------------------------------------------------------------------------------------
    #region Constants
    protected static readonly Vector3   kXAxis;		    // points in the directon of the positive X axis
    protected static readonly Vector3   kZAxis;		    // points in the direction of the positive Y axis
    private static readonly float       kDepth;			// used for intersection tests done in 3D.
    #endregion


    #region Fields
    protected int                       m_numberOfRows;
    protected int                       m_numberOfColumns;
    protected float                     m_cellSize;
    private Vector3                     m_origin;
    #endregion


    /// -----------------------------------------------------------------------------------------
    /// <summary>
    /// 
    /// </summary>
    /// ----------------------------------------------------------------------------------------
    public float Width
    {
        get { return m_numberOfColumns * m_cellSize; }
    }

    /// -----------------------------------------------------------------------------------------
    /// <summary>
    /// 
    /// </summary>
    /// ----------------------------------------------------------------------------------------
    public float Height
    {
        get { return m_numberOfRows * m_cellSize; }
    }

    /// -----------------------------------------------------------------------------------------
    /// <summary>
    /// 
    /// </summary>
    /// ----------------------------------------------------------------------------------------
    public Vector3 Origin
    {
        get { return m_origin; }
    }

    /// -----------------------------------------------------------------------------------------
    /// <summary>
    /// 
    /// </summary>
    /// ----------------------------------------------------------------------------------------
    public float Left
    {
        get { return m_origin.x; }
    }

    /// -----------------------------------------------------------------------------------------
    /// <summary>
    /// 
    /// </summary>
    /// ----------------------------------------------------------------------------------------
    public float Right
    {
        get { return m_origin.x + Width; }
    }

    /// -----------------------------------------------------------------------------------------
    /// <summary>
    /// 
    /// </summary>
    /// ----------------------------------------------------------------------------------------
    public float Top
    {
        get { return m_origin.z + Height; }
    }

    /// -----------------------------------------------------------------------------------------
    /// <summary>
    /// 
    /// </summary>
    /// ----------------------------------------------------------------------------------------
    public float Bottom
    {
        get { return m_origin.z; }
    }

    /// -----------------------------------------------------------------------------------------
    /// <summary>
    /// 
    /// </summary>
    /// ----------------------------------------------------------------------------------------
    public float CellSize
    {
        get { return m_cellSize; }
    }

    public int NumberOfCells
    {
        get { return m_numberOfRows * m_numberOfColumns; }
    }


    static CGrid()
	{
		kXAxis = new Vector3(1.0f, 0.0f, 0.0f); 
		kZAxis = new Vector3(0.0f, 0.0f, 1.0f);
		kDepth = 1.0f;
	}

    // Use this for initialization
    public virtual void Awake(Vector3 origin, int numRows, int numCols, float cellSize, bool show)
    {
        m_origin            = origin;
        m_numberOfRows      = numRows;
        m_numberOfColumns   = numCols;
        m_cellSize          = cellSize;
    }

    public static void DebugDraw(Vector3 origin, int numRows, int numCols, float cellSize, Color color)
    {
        float width         = (numCols * cellSize);
        float height        = (numRows * cellSize);

        // Draw the horizontal grid lines
        for (int i = 0; i < numRows + 1; i++)
        {
            Vector3 startPos    = origin + i * cellSize * kZAxis;
            Vector3 endPos      = startPos + width * kXAxis;
            Debug.DrawLine(startPos, endPos, color);
        }

        // Draw the vertial grid lines
        for (int i = 0; i < numCols + 1; i++)
        {
            Vector3 startPos    = origin + i * cellSize * kXAxis;
            Vector3 endPos      = startPos + height * kZAxis;
            Debug.DrawLine(startPos, endPos, color);
        }
    }

    // returns a position in world space coordinates.
    public Vector3 GetCellCenter(int index)
    {
        Vector3 cellPosition = GetCellPosition(index);
        cellPosition.x      += (m_cellSize / 2.0f);
        cellPosition.z      += (m_cellSize / 2.0f);
        return cellPosition;
    }

    /// <summary>
    /// Returns the lower left position of the grid cell at the passed tile index. The origin of the grid is at the lower left,
    /// so it uses a cartesian coordinate system.
    /// </summary>
    /// <param name="index">index to the grid cell to consider</param>
    /// <returns>Lower left position of the grid cell (origin position of the grid cell), in world space coordinates</returns>
    public Vector3 GetCellPosition(int index)
    {
        int row = GetRow(index);
        int col = GetColumn(index);
        float x = col * m_cellSize;
        float z = row * m_cellSize;
        Vector3 cellPosition = Origin + new Vector3(x, 0.0f, z);
        return cellPosition;
    }

    // pass in world space coords. Get the tile index at the passed position
    public int GetCellIndex(Vector3 pos)
    {
        if (!IsInBounds(pos))
        {
            return -1;
        }

        pos -= Origin;

        int col = (int)(pos.x / m_cellSize);
        int row = (int)(pos.z / m_cellSize);

        return (row * m_numberOfColumns + col);
    }

    public int GetRow(int index)
    {
        int row = index / m_numberOfColumns;
        return row;
    }

    public int GetColumn(int index)
    {
        int col = index % m_numberOfColumns;
        return col;
    }

    public bool IsInBounds(int col, int row)
    {
        if (col < 0 || col >= m_numberOfColumns)
        {
            return false;
        }
        else if (row < 0 || row >= m_numberOfRows)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool IsInBounds(int index)
    {
        return (index >= 0 && index < NumberOfCells);
    }

    // pass in world space coords
    public bool IsInBounds(Vector3 pos)
    {
        return (pos.x >= Left &&
                 pos.x <= Right &&
                 pos.z <= Top &&
                 pos.z >= Bottom);
    }
}

