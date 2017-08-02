#!/bin/bash

USER=${MONGODB_USER:-"ipUser"}
DATABASE=${MONGODB_DATABASE:-"InstaPost"}
PASS=${MONGODB_PASS:-"Testtest/123"}
_word=$( [ ${MONGODB_PASS} ] && echo "preset" || echo "random" )

RET=1
while [[ RET -ne 0 ]]; do
    echo "=> Waiting for confirmation of MongoDB service startup"
    sleep 5
    mongo admin --eval "help" >/dev/null 2>&1
    RET=$?
done

echo "=> Creating an ${USER} user with a ${_word} password in MongoDB"

mongo admin << EOF
use admin;
db.createUser({
    user: 'useradmin',
    pwd: '${PASS}',
    roles:[{
        role:'userAdminAnyDatabase',
        db:'admin'
    }]
});
exit
EOF

mongo admin -u useradmin -p ${PASS} << EOF
use InstaPost;
db.createUser({
    user: ${USER},
    pwd: ${PASS},
    roles:[{
        role:'readWrite',
        db:${DATABASE}
    }]
});
exit
EOF

echo "Creating skeletal collections..."
. /mongo_db_script.sh

touch /data/db/.mongodb_password_set

echo "You can now connect to this MongoDB server using:"
echo ""
echo "    mongo $DATABASE -u $USER -p $PASS --host <host> --port <port>"
echo ""
echo "Please remember to change the above password as soon as possible!"
echo "========================================================================"