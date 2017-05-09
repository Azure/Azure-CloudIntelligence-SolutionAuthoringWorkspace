module.exports = function (context, req) {
    context.log('JavaScript HTTP trigger function processed a request.');
    
    var name = context.bindings.req.body.name;

    context.bindings.res = { message: 'Hello, ' + name + '!' };
    
    context.done();
};