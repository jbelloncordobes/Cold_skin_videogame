# !/bin/bash

# Check if the 2D project has any URP or HDRP packages installed despite no pipeline asset assigned
cat "/Users/xavicanadas/Archivos/Projectes/Uni/4t curs/NaVi/Cold_skin_videogame/Packages/manifest.json" | grep -i "render\|pipeline\|urp\|hdrp"

# Same for 3D
cat "/Users/xavicanadas/Downloads/NAVI_Demo/Packages/manifest.json" | grep -i "render\|pipeline\|urp\|hdrp"