<?php
    $con = mysqli_connect('localhost','root','','unity');

    if(mysqli_connect_errno())
    {
        echo "1: Connection failed";
        exit();
    }

    $username = $_POST["username"];

    $password = $_POST["password"];

    $namecheck = mysqli_query($con, "SELECT username FROM players WHERE username='" . $username . "';") or die("2: Name check query failed");

    if(mysqli_num_rows($namecheck) != 1)
    {
        echo "5: Bad credentials";
        exit();
    }

    $passwordcheck = mysqli_query($con, "SELECT username FROM players WHERE username='" . $username . "' AND password='" . $password . "';") or die("7: Login query failed");

    if(mysqli_num_rows($passwordcheck) != 1)
    {
        echo "6: Bad credentials";
        exit();
    }


    echo "0";
?>



