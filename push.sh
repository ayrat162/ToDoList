#!/bin/bash
echo "Enter commit text:"
read -r text
echo ""
if [ "$text" != "" ]; then
	git add .
	git commit -m "$text"
	git push origin master
else
	echo "ERROR: Empty commit text!"
	./push.sh
fi
