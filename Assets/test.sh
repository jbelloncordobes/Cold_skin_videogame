# /bin/bash
# List all .cs filenames from both projects side by side
echo "=== 2D ===" && find "/Users/xavicanadas/Archivos/Projectes/Uni/4t curs/NaVi/Cold_skin_videogame/Assets" -name "*.cs" | sed 's|.*/||' | sort > /tmp/2d_scripts.txt && cat /tmp/2d_scripts.txt
echo "=== 3D ===" && find "/Users/xavicanadas/Downloads/NAVI_Demo/Assets" -name "*.cs" | sed 's|.*/||' | sort > /tmp/3d_scripts.txt && cat /tmp/3d_scripts.txt

# Then check for conflicts
echo "=== CONFLICTS ===" && comm -12 /tmp/2d_scripts.txt /tmp/3d_scripts.txt

# See your scene files
find "/Users/xavicanadas/Archivos/Projectes/Uni/4t curs/NaVi/Cold_skin_videogame" -name "*.unity" | sed 's|.*/||'
find "/Users/xavicanadas/Downloads/NAVI_Demo" -name "*.unity" | sed 's|.*/||'

# Show the GameManagerSetup.cs content
cat "/Users/xavicanadas/Archivos/Projectes/Uni/4t curs/NaVi/Cold_skin_videogame/Assets/Scripts/GameManagerSetup.cs"

cp "/Users/xavicanadas/Downloads/NAVI_Demo/Assets/Scripts/*.cs" "/Users/xavicanadas/Archivos/Projectes/Uni/4t curs/NaVi/Cold_skin_videogame/Assets/Scripts/Night/"