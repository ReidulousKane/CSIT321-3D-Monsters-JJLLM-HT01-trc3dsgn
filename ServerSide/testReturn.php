<?php
	$db = mysqli_connect("localhost", "root", "root") or die("Could not connect: " . mysqli_error()); 
    mysqli_select_db($db, "test") or die("Could not select database");
 
    $query = "SELECT testString FROM testConnection ORDER by id DESC LIMIT 1";
    $sqlResult = mysqli_query($db, $query) or die("Query failed: " . mysql_error());
    $result = mysqli_fetch_row($sqlResult);

    echo "Server returned: $result[0]";