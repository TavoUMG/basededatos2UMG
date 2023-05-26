function GetCount(tablename) {
    var count = 0;
    $("#" + tablename+" TBODY TR").each(function () {
        count++;
    });
    return count;
}

function AddCell(row, value)
{
    var cell = $(row.insertCell(-1));
    cell.html(value);
    return cell;
}

function CreateHiddenField(listname, fieldname, value, count) {
    var hiddenField = $("<input />");
    hiddenField.attr("type", "hidden");
    hiddenField.attr("id", listname + "_" + count + "__" + fieldname);
    hiddenField.attr("name", listname + "[" + count + "]." + fieldname);
    hiddenField.val(value);
    return hiddenField;
}

function CreateRemoveButton(methodname) {
    var btnRemove = $("<input />");
    btnRemove.attr("type", "button");
    btnRemove.attr("class", "btn btn-secondary");
    btnRemove.attr("onclick", methodname+"(this);");
    btnRemove.val("Quitar");
    return btnRemove;
}

function FixNamesId(field, listname, fieldname, count) {
    field.attr("id", listname + "_" + count + "__" + fieldname);
    field.attr("name", listname + "[" + count + "]." + fieldname);
    return field;
}

function Check(tablename, value, index) {
    var checkResult = true;

    if (value == null) {
        checkResult = false;
    }
    else if (value == "") {
        checkResult = false;
    }
    else {
        $("#" + tablename + " TBODY TR").each(function () {
            var row = $(this);
            var idData = row.find("input").eq(index);

            if (value == idData.val())
                checkResult = false;
        });
    }
    return checkResult;
}