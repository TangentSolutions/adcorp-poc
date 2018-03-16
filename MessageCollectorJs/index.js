module.exports = function (context, req) {
    var message = {
        name: req.body.name,
        cellPhone: req.body.cellPhone,
        content: req.body.message,
        uuid: Math.floor((1 + Math.random()) * 0x10000).toString(16).substring(1)
    };
    
    errors = [];

    if (!message.name) { errors.push('name'); }
    if (!message.cellPhone) { errors.push('cellPhone'); }
    if (!message.content) { errors.push('content'); }

    if (errors.length > 0) {
        context.res = {
            status: 400,
            body: "Missing parameters: " + errors.join(', ')
        };
    } else {
        context.res = {
            // status: 200, /* Defaults to 200 */
            body: message
        };
    }

    context.done();
};