class CSharpInterface:
    def split_list(a_list):
        half = len(a_list)//2
        return a_list[:half], a_list[half:]


    def TrainNetwork(input):
        [XTrain, XTest] = split_list(input)

    def returnHello(name):
        return 'Hello, ' + name