using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
	class Snake : Figure
	{
		Direction direction;
        ConsoleKey oldKey = ConsoleKey.LeftArrow;

        public Snake( Point tail, int length, Direction _direction )
		{
			direction = _direction;
			pList = new List<Point>();
			for ( int i = 0; i < length; i++ )
			{
				Point p = new Point( tail );
				p.Move( i, direction );
				pList.Add( p );
			}
		}

		public void Move()
		{
			Point tail = pList.First();			
			pList.Remove( tail );
			Point head = GetNextPoint();
			pList.Add( head );

			tail.Clear();
			head.Draw();
		}

		public Point GetNextPoint()
		{
			Point head = pList.Last();
			Point nextPoint = new Point( head );
			nextPoint.Move( 1, direction );
			return nextPoint;
		}

		public bool IsHitTail()
		{
			var head = pList.Last();
			for(int i = 0; i < pList.Count - 2; i++ )
			{
				if ( head.IsHit( pList[ i ] ) )
					return true;
			}
			return false;
		}

		public void HandleKey(ConsoleKey key)
		{
			if ( key == ConsoleKey.LeftArrow )
            {
                if (oldKey != ConsoleKey.RightArrow)
                {
				    direction = Direction.LEFT;
                    oldKey = key;
                }
            }
			else if ( key == ConsoleKey.RightArrow )
            {
                if (oldKey != ConsoleKey.LeftArrow)
                {
                    direction = Direction.RIGHT;
                    oldKey = key;
                }
            }
			else if ( key == ConsoleKey.DownArrow )
            {
                if (oldKey != ConsoleKey.UpArrow)
                {
                    direction = Direction.DOWN;
                    oldKey = key;
                }
            }
			else if ( key == ConsoleKey.UpArrow )
            {
                if (oldKey != ConsoleKey.DownArrow)
                {
                    direction = Direction.UP;
                    oldKey = key;
                }
            }
        }

		public bool Eat( Point food )
		{
			Point head = GetNextPoint();
			if ( head.IsHit( food ) )
			{
				food.sym = head.sym;
				pList.Add( food );
				return true;
			}
			else
				return false;
		}
	}
}
